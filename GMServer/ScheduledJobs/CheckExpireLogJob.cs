using Quartz;
using GMServer.Services;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;
using NGEL.Data;
using Newtonsoft.Json;

namespace GMServer.ScheduledJobs
{
    public class CheckExpireLogJob : IJob
    {
        private readonly ILogger<CheckExpireLogJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly ServerStateService _serverStateService;
        private readonly GMLogService _gmLogService;
        public CheckExpireLogJob(ILogger<CheckExpireLogJob> logger, DBHelper dbHelper, CommonSettings commonSettings, IWebHostEnvironment environment, ServerStateService serverStateService, GMLogService gmLogService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _commonSettings = commonSettings;
            _environment = environment;
            _serverStateService = serverStateService;
            _gmLogService = gmLogService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (false == _serverStateService.isMasterServer)
                return;

            _logger.LogDebug($"[Start] CheckExpireLog Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}", ConsoleColor.Cyan);

            try
            {
                NGEL.Data.Models.Settings? expireLogSetting = new NGEL.Data.Models.Settings
                {
                    key = "ExpireLog",
                    value = 30,
                };

                var selectExpireLogSettingTask = await _dbHelper.GameManager.SelectExpireLogSetting();
                if (selectExpireLogSettingTask.Item1 && null != selectExpireLogSettingTask.Item2)
                {
                    expireLogSetting = selectExpireLogSettingTask.Item2;
                }
                else
                {
                    var saveExpireLogSettingTask = await _dbHelper.GameManagerWriteOnly.SaveExpireLogSetting(expireLogSetting);
                }

                var expireDate = DateTime.UtcNow.AddDays(-expireLogSetting.value);
                var selectExpireLogCountTask = await _dbHelper.GMLog.SelectExpiredLogCount(expireDate);
                if (selectExpireLogCountTask.Item1 && selectExpireLogCountTask.Item2 > 0)
                {
                    var deleteExpireLogTask = await _dbHelper.GMLogWriteOnly.DeleteExpiredLogs(expireDate);
                    var deleteLog = new Dictionary<string, string>
                    {
                        { "count", selectExpireLogCountTask.Item2.ToString() },
                        { "expireDate", expireDate.ToString("yyyy-MM-dd HH:mm:ss") }
                    };
                    await _gmLogService.InsertSchedulerLog("DeleteExpiredLogs", "", JsonConvert.SerializeObject(deleteLog));
                    _logger.LogDebug($"[Done] CheckExpireLog Job: DeleteExpiredLogs - {JsonConvert.SerializeObject(deleteLog)}", ConsoleColor.Cyan);
                }

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            _logger.LogDebug($"[End] CheckExpireLog Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}", ConsoleColor.Cyan);
        }
    }
}
