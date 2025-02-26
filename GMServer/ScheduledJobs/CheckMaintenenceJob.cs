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
    public class CheckMaintenenceJob : IJob
    {
        private readonly ILogger<CheckMaintenenceJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly StompService _stompService;
        private readonly MaintenanceService _maintenanceService;
        private readonly ServerStateService _serverStateService;

        public CheckMaintenenceJob(ILogger<CheckMaintenenceJob> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment, StompService stompService, MaintenanceService maintenanceService, ServerStateService serverStateService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
            _stompService = stompService;
            _maintenanceService = maintenanceService;
            _serverStateService = serverStateService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (false == _serverStateService.isMasterServer)
                return;

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[Start] CheckMaintenence Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, maintenence count: {_maintenanceService.maintenances.Count}", ConsoleColor.Yellow);

            try
            {
                var selectMaintenancesTask = await _dbHelper.Admin.SelectMaintenances();
                if (selectMaintenancesTask.Item1 && null != selectMaintenancesTask.Item2 && 0 < selectMaintenancesTask.Item2.Count)
                {
                    foreach (var maintenance in selectMaintenancesTask.Item2)
                    {
                        var existsData = _maintenanceService.maintenances.Find(_ => _.MaintenanceID == maintenance.MaintenanceID);
                        if (null == existsData)
                            _maintenanceService.AddMaintenance(maintenance);

                        if (Defines.MaintenanceState.Maintenance == maintenance.State)
                        {
                            if (DateTime.UtcNow.CompareTo(maintenance.EndTime) > 0)
                            {
                                maintenance.State = Defines.MaintenanceState.Service;
                                if (await _dbHelper.AdminWriteOnly.UpdateMaintenance(maintenance))
                                {
                                    if (_environment.IsDevelopment())
                                        _logger.LogInformation($"[maintenanceID: {maintenance.MaintenanceID}] state is turned on service.");
                                }
                                continue;
                            }

                            if (false == maintenance.isKicked && DateTime.UtcNow.CompareTo(maintenance.StartTime) >= 0 && DateTime.UtcNow.CompareTo(maintenance.EndTime) <= 0)
                            {
                                await _stompService.PubMaintenanceKick(maintenance.Platform);
                                _maintenanceService.CheckKick(maintenance);
                            }
                        }
                    }
                }

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[End] CheckMaintenence Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, maintenence count: {_maintenanceService.maintenances.Count}", ConsoleColor.Yellow);
        }
    }
}
