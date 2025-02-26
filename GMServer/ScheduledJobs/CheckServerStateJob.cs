using GMServer.Services;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;
using Lobby;
using Quartz;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GMServer.ScheduledJobs
{
    public class CheckServerStateJob : IJob
    {
        private readonly ILogger<CheckServerStateJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly ServerStateService _serverStateService;
        private readonly GMUserService _gmUserService;

        public CheckServerStateJob(ILogger<CheckServerStateJob> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment, ServerStateService serverStateService, GMUserService gmUserService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
            _serverStateService = serverStateService;
            _gmUserService = gmUserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var checkMasterServerTask = await _dbHelper.GameManagerWriteOnly.CheckMasterServer(_serverStateService.serverId);

                if (checkMasterServerTask.Item1 && null != checkMasterServerTask.Item2 && checkMasterServerTask.Item2.isMaster)
                {
                    if (false == _serverStateService.isMasterServer)
                    {
                        _ = _dbHelper.GameManagerWriteOnly.ClearIdleServer();
                        if (_environment.IsDevelopment())
                            _logger.LogInformation($"server({_serverStateService.serverId}) is master.");
                    }

                    _serverStateService.SetMasterServer();
                }
                else
                {
                    _serverStateService.ClearMasterServer();
                }

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }
    }
}
