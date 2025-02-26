using GMServer.Hubs;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Helpers;
using NGEL.Data.Models;
using NGEL.Data.Settings;
using static Quartz.Logging.OperationName;

namespace GMServer.Services
{
    public class GMUserService
    {
        private readonly ILogger<GMUserService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthorizationService _authorizationService;
        private readonly CommonSettings _commonSettings;
        private readonly AutoGMServerHubSend _autoGMServerHubSend;
        private object lockObject = new object();

        private Dictionary<string, GMUser?> _connections;

        public GMUserService(ILogger<GMUserService> logger, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService, CommonSettings commonSettings, NavMenuSettings navMenuSettings, AutoGMServerHubSend autoGMServerHubSend)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _authorizationService = authorizationService;
            _commonSettings = commonSettings;
            _autoGMServerHubSend = autoGMServerHubSend;
            lock(lockObject)
            {
                if (null == _connections)
                    _connections = new Dictionary<string, GMUser?>();
            }
        }

        public Errors UpdateConnectedUser(string connectionId, OAuthSignInUser? user)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                _logger.LogError($"ConnectionIdRequired: ${connectionId}");
                return Errors.GMUserService_ConnectionIdRequired;
            }

            try
            {
                lock (lockObject)
                {
                    if (_connections.TryGetValue(connectionId, out var connectedUser))
                    {
                        if (null != user)
                        {
                            if (null == connectedUser)
                            {
                                connectedUser = user.GetGMUser();
                            }
                            else
                            {
                                connectedUser.token = user.token;
                                connectedUser.countFailedSignin = user.countFailedSignin;
                                connectedUser.latestSignin = user.latestSignin;
                                connectedUser.latestSignout = user.latestSignout;
                                connectedUser.updatedTime = user.updatedTime;
                            }

                            _connections[connectionId] = connectedUser;
                        }
                    }
                    else
                    {
                        GMUser? gmUser = null;
                        if (null != user)
                            gmUser = user.GetGMUser();

                        _connections.Add(connectionId, gmUser);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToUpdateConnectedUser;
            }

            return Errors.None;
        }

        public Errors RemoveConnectedUser(string connectionId)
        {
            try
            {
                lock (lockObject)
                {
                    if (_connections.TryGetValue(connectionId, out var connectedUser))
                    {
                        _connections.Remove(connectionId);
                    }
                    else
                    {
                        _logger.LogError($"Not Found Connected User: ${connectionId}");
                        return Errors.GMUserService_NotFoundUser;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToRemoveConnectedUser;
            }

            return Errors.None;
        }

        public Errors GetUsers(out Dictionary<string, GMUser?> users)
        {
            users = new Dictionary<string, GMUser?>();

            try
            {
                lock (lockObject)
                {
                    users = _connections;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToGetConnectedUsers;
            }

            return Errors.None;
        }

        public Errors GetUser(string connectionId, out GMUser? user)
        {
            user = null;

            if (string.IsNullOrWhiteSpace(connectionId))
                return Errors.GMUserService_ConnectionIdRequired;

            try
            {
                lock (lockObject)
                {
                    _connections.TryGetValue(connectionId, out user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToGetConnectedUsers;
            }

            return Errors.None;
        }

        public Errors GetUserJobs(string connectionId, out Dictionary<Guid, UserJob> jobs)
        {
            jobs = new Dictionary<Guid, UserJob>();

            if (string.IsNullOrWhiteSpace(connectionId))
                return Errors.GMUserService_ConnectionIdRequired;

            try
            {
                lock (lockObject)
                {
                    if (false == _connections.TryGetValue(connectionId, out var user) || null == user)
                    {
                        return Errors.GMUserService_NotFoundUser;
                    }
                    else
                    {
                        jobs = user.userJobs;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToGetUserJobs;
            }

            return Errors.None;
        }

        public Errors GetUserJob(string connectionId, Guid jobId, out UserJob? userJob)
        {
            userJob = null;

            if (string.IsNullOrWhiteSpace(connectionId) || Guid.Empty == jobId)
                return Errors.GMUserService_ConnectionIdAndJobIdRequired;

            try
            {
                lock (lockObject)
                {
                    _connections.TryGetValue(connectionId, out var user);
                    if (null == user || null == user.userJobs || 1 > user.userJobs.Count)
                    {
                        return Errors.GMUserService_NotFoundUser;
                    }
                    else if (false == user.userJobs.TryGetValue(jobId, out userJob) || null == userJob)
                    {
                        return Errors.GMUserService_NotFoundUserJob;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToGetUserJobs;
            }

            return Errors.None;
        }

        public Errors AddUserJobCount(string connectionId, UserJob userJob, bool isFailed)
        {
            if (string.IsNullOrWhiteSpace(connectionId) || Guid.Empty == userJob.id)
                return Errors.GMUserService_ConnectionIdAndJobIdRequired;

            try
            {
                lock (lockObject)
                {
                    _connections.TryGetValue(connectionId, out var user);
                    if (null == user)
                    {
                        return Errors.GMUserService_NotFoundUser;
                    }
                    else if (user.userJobs.TryGetValue(userJob.id, out var existsUserJob))
                    {
                        if (existsUserJob.jobCount == (existsUserJob.succeededCount + existsUserJob.failedCount))
                            return Errors.GMUserService_CompletedJob;

                        if (isFailed)
                        {
                            existsUserJob.failedCount++;
                        }
                        else
                        {
                            existsUserJob.succeededCount++;
                        }

                        if (false == string.IsNullOrWhiteSpace(userJob.message))
                            existsUserJob.message += (string.IsNullOrWhiteSpace(existsUserJob.message) ? "" : "\n") + userJob.message;

                        existsUserJob.updateTime = DateTime.UtcNow;
                        user.userJobs[userJob.id] = existsUserJob;
                    }
                    else if (Defines.UserJobType.None == userJob.jobType)
                    {
                        return Errors.GMUserService_UserJobTypeRequired;
                    }
                    else if (1 > userJob.jobCount)
                    {
                        return Errors.GMUserService_UserJobCountRequired;
                    }
                    else
                    {
                        user.userJobs.Add(userJob.id, userJob);
                    }
                    _connections[connectionId] = user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToAddUserJobCount;
            }

            return Errors.None;
        }

        public Errors ClearUserJob(string connectionId, Guid jobId)
        {
            if (string.IsNullOrWhiteSpace(connectionId) || Guid.Empty == jobId)
                return Errors.GMUserService_ConnectionIdAndJobIdRequired;

            try
            {
                lock (lockObject)
                {
                    _connections.TryGetValue(connectionId, out var user);
                    if (null == user)
                    {
                        return Errors.GMUserService_NotFoundUser;
                    }
                    else if (user.userJobs.TryGetValue(jobId, out var existsUserJob))
                    {
                        if (existsUserJob.jobCount != (existsUserJob.succeededCount + existsUserJob.failedCount))
                            return Errors.GMUserService_OnProcessingJob;

                        user.userJobs.Remove(jobId);
                    }
                    else
                    {
                        return Errors.GMUserService_NotFoundUserJob;
                    }
                    _connections[connectionId] = user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return Errors.GMUserService_FailedToClearUserJob;
            }

            return Errors.None;
        }
    }
}
