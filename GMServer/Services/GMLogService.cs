using Microsoft.AspNetCore.Authentication;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Helpers;
using NGEL.Data.Models;
using System.Security.Claims;

namespace GMServer.Services
{
    public class GMLogService
    {
        private readonly ILogger<GMLogService> _logger;
        private readonly Dictionary<string, Int64> _logMethods;
        private readonly Dictionary<string, Int64> _logURL;
        private readonly Dictionary<string, Int64> _logErrors;
        private readonly Dictionary<string, Int64> _logUserAgents;
        private readonly DBHelper _dbHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private object lockErrorObject = new object();
        private object lockMethodObject = new object();
        private object lockURLObject = new object();
        private object lockUserAgentObject = new object();

        public GMLogService(ILogger<GMLogService> logger, DBHelper dbHelper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _logMethods = new Dictionary<string, Int64>();
            _logURL = new Dictionary<string, Int64>();
            _logErrors = new Dictionary<string, Int64>(); ;
            _logUserAgents = new Dictionary<string, Int64>();
            _dbHelper = dbHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Int64> AddMethod(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return 0;

            if (_logMethods.TryGetValue(name, out var existsId))
                return existsId;

            var selectLogMethodTask = await _dbHelper.GMLog.SelectLogMethodByName(name);

            if (false == selectLogMethodTask.Item1 || null == selectLogMethodTask.Item2)
            {
                Int64 id = 1;

                if (0 < _logMethods.Count)
                {
                    var maxId = _logMethods.Max(_ => _.Value);
                    id = maxId + 1;
                }

                if (await _dbHelper.GMLogWriteOnly.InsertLogMethod(id, name))
                {
                    lock (lockMethodObject)
                        _logMethods.Add(name, id);

                    return id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                lock (lockMethodObject)
                    _logMethods.Add(selectLogMethodTask.Item2.name, selectLogMethodTask.Item2.id);

                return selectLogMethodTask.Item2.id;
            }
        }

        private async Task<Int64> AddURL(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return 0;

            if (_logURL.TryGetValue(name, out var existsId))
                return existsId;

            var selectLogURLTask = await _dbHelper.GMLog.SelectLogURLByName(name);
            if (false == selectLogURLTask.Item1 || null == selectLogURLTask.Item2)
            {
                Int64 id = 1;

                if (0 < _logURL.Count)
                {
                    var maxId = _logURL.Max(_ => _.Value);
                    id = maxId + 1;
                }

                if (await _dbHelper.GMLogWriteOnly.InsertLogURL(id, name))
                {
                    lock (lockURLObject)
                        _logURL.Add(name, id);

                    return id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                lock (lockURLObject)
                    _logURL.Add(selectLogURLTask.Item2.name, selectLogURLTask.Item2.id);

                return selectLogURLTask.Item2.id;
            }
        }

        private async Task<Int64> AddError(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return 0;

            if (_logErrors.TryGetValue(name, out var existsId))
                return existsId;

            var selectLogErrorTask = await _dbHelper.GMLog.SelectLogErrorByName(name);

            if (false == selectLogErrorTask.Item1 || null == selectLogErrorTask.Item2)
            {
                Int64 id = 1;

                if (0 < _logErrors.Count)
                {
                    var maxId = _logErrors.Max(_ => _.Value);
                    id = maxId + 1;
                }

                if (await _dbHelper.GMLogWriteOnly.InsertLogError(id, name))
                {
                    lock (lockErrorObject)
                        _logErrors.Add(name, id);

                    return id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                lock (lockErrorObject)
                    _logErrors.Add(selectLogErrorTask.Item2.name, selectLogErrorTask.Item2.id);

                return selectLogErrorTask.Item2.id;
            }
        }

        private async Task<Int64> AddUserAgent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return 0;

            if (_logUserAgents.TryGetValue(name, out var existsId))
                return existsId;

            var selectLogUserAgentTask = await _dbHelper.GMLog.SelectLogUserAgentByName(name);

            if (false == selectLogUserAgentTask.Item1 || null == selectLogUserAgentTask.Item2)
            {
                Int64 id = 1;

                if (0 < _logUserAgents.Count)
                {
                    var maxId = _logUserAgents.Max(_ => _.Value);
                    id = maxId + 1;
                }

                if (await _dbHelper.GMLogWriteOnly.InsertLogUserAgent(id, name))
                {
                    lock (lockUserAgentObject)
                        _logUserAgents.Add(name, id);

                    return id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                lock (lockUserAgentObject)
                    _logUserAgents.Add(selectLogUserAgentTask.Item2.name, selectLogUserAgentTask.Item2.id);

                return selectLogUserAgentTask.Item2.id;
            }
        }

        public async Task<Int64> GetMethodID(string name)
        {
            if (_logMethods.TryGetValue(name, out var id))
            {
                _ = _dbHelper.GMLog.SelectLogMethodByName(name).ContinueWith((antecedent) =>
                {
                    if (antecedent.IsCompleted)
                    {
                        if (antecedent.Result.Item1 && null == antecedent.Result.Item2)
                        {
                            _ = _dbHelper.GMLogWriteOnly.InsertLogMethod(id, name);
                        }
                    }
                });
                return id;
            }
            else
            {
                return await AddMethod(name);
            }
        }

        public async Task<Int64> GetURLID(string name)
        {
            if (_logURL.TryGetValue(name, out var id))
            {
                _ = _dbHelper.GMLog.SelectLogURLByName(name).ContinueWith((antecedent) =>
                {
                    if (antecedent.IsCompleted)
                    {
                        if (antecedent.Result.Item1 && null == antecedent.Result.Item2)
                        {
                            _ = _dbHelper.GMLogWriteOnly.InsertLogURL(id, name);
                        }
                    }
                });
                return id;
            }
            else
            {
                return await AddURL(name);
            }
        }

        public async Task<Int64> GetErrorID(string name)
        {
            if (_logErrors.TryGetValue(name, out var id))
            {
                return id;
            }
            else
            {
                return await AddError(name);
            }
        }

        public async Task<Int64> GetUserAgentID(string name)
        {
            if (_logUserAgents.TryGetValue(name, out var id))
            {
                return id;
            }
            else
            {
                return await AddUserAgent(name);
            }
        }

        public async Task<bool> Init()
        {
            var selectLogMethodsTask = await _dbHelper.GMLog.SelectLogMethods();
            if (false == selectLogMethodsTask.Item1)
            {
                return false;
            }

            var selectLogURLTask = await _dbHelper.GMLog.SelectLogURLs();
            if (false == selectLogURLTask.Item1)
            {
                return false;
            }

            var selectLogErrorsTask = await _dbHelper.GMLog.SelectLogErrors();
            if (false == selectLogErrorsTask.Item1)
            {
                return false;
            }

            var selectLogUserAgentsTask = await _dbHelper.GMLog.SelectLogUserAgents();
            if (false == selectLogUserAgentsTask.Item1)
            {
                return false;
            }

            if (null != selectLogMethodsTask.Item2 && 0 < selectLogMethodsTask.Item2.Count)
            {
                foreach (var item in selectLogMethodsTask.Item2)
                {
                    if (_logMethods.ContainsKey(item.name))
                        continue;

                    _logMethods.Add(item.name, item.id);
                }
            }

            if (null != selectLogURLTask.Item2 && 0 < selectLogURLTask.Item2.Count)
            {
                foreach (var item in selectLogURLTask.Item2)
                {
                    if (_logURL.ContainsKey(item.name))
                        continue;

                    _logURL.Add(item.name, item.id);
                }
            }

            if (null != selectLogErrorsTask.Item2 && 0 < selectLogErrorsTask.Item2.Count)
            {
                foreach (var item in selectLogErrorsTask.Item2)
                {
                    if (_logErrors.ContainsKey(item.name))
                        continue;

                    _logErrors.Add(item.name, item.id);
                }
            }

            if (null != selectLogUserAgentsTask.Item2 && 0 < selectLogUserAgentsTask.Item2.Count)
            {
                foreach (var item in selectLogUserAgentsTask.Item2)
                {
                    if (_logUserAgents.ContainsKey(item.name))
                        continue;

                    _logUserAgents.Add(item.name, item.id);
                }
            }

            return true;
        }

        public async Task InsertAPILog(string userId, string method, string url, string error, string userAgent, string message, string remoteAddress)
        {
            if (string.IsNullOrWhiteSpace(method))
                return;

            var methodId = await GetMethodID(method);

            if (1 > methodId)
                return;

            var urlId = await GetURLID(url);

            if (1 > urlId)
                return;

            var errorId = await GetErrorID(error);

            var userAgentId = await GetUserAgentID(userAgent);

            if (string.IsNullOrWhiteSpace(url))
                return;

            await _dbHelper.GMLogWriteOnly.InsertLog(userId, Defines.GMLogType.API, methodId, urlId, errorId, userAgentId, message, remoteAddress);
        }

        public async Task InsertHubReqLog(string userId, string method, string url, string error, string userAgent, string message, string remoteAddress)
        {
            if (string.IsNullOrWhiteSpace(method))
                return;

            var methodId = await GetMethodID(method);

            if (1 > methodId)
                return;

            var urlId = await GetURLID(url);

            var errorId = await GetErrorID(error);

            var userAgentId = await GetUserAgentID(userAgent);

            await _dbHelper.GMLogWriteOnly.InsertLog(userId, Defines.GMLogType.HubReq, methodId, urlId, errorId, userAgentId, message, remoteAddress);
        }

        public async Task InsertSchedulerLog(string method, string error, string message)
        {
            await InsertSchedulerLog("", method, "", error, "", message, "");
        }

        public async Task InsertSchedulerLog(string userId, string method, string url, string error, string userAgent, string message, string remoteAddress)
        {
            if (string.IsNullOrWhiteSpace(method))
                return;

            var methodId = await GetMethodID(method);

            if (1 > methodId)
                return;

            var urlId = await GetURLID(url);

            var errorId = await GetErrorID(error);

            var userAgentId = await GetUserAgentID(userAgent);

            await _dbHelper.GMLogWriteOnly.InsertLog(userId, Defines.GMLogType.Scheduler, methodId, urlId, errorId, userAgentId, message, remoteAddress);
        }

        public async Task InsertSTOMPLog(string method, string body, string destination)
        {
            await InsertSTOMPLog("", method, "", body, destination, "", "");
        }

        public async Task InsertSTOMPLog(string userId, string method, string body, string destination, string url, string remoteAddress)
        {
            await InsertSTOMPLog(userId, method, "", body, destination, url, remoteAddress);
        }

        public async Task InsertSTOMPLog(string userId, string method, string error, string body, string destination, string url, string remoteAddress)
        {
            if (string.IsNullOrWhiteSpace(method))
                return;

            var methodId = await GetMethodID($"STOMP_{method}");

            if (1 > methodId)
                return;

            var urlId = await GetURLID(url);

            var errorId = await GetErrorID(error);

            var message = "";
            if (false == string.IsNullOrWhiteSpace(body))
                message += $"\"body\":{body}";

            if (false == string.IsNullOrWhiteSpace(destination))
                message += $"{(string.IsNullOrWhiteSpace(body) ? "" : ",")}\"destination\":\"{destination}\"";

            message = $"{{{message}}}";

            await _dbHelper.GMLogWriteOnly.InsertLog(userId, Defines.GMLogType.STOMP, methodId, urlId, errorId, 0, message, remoteAddress);
        }
    }
}
