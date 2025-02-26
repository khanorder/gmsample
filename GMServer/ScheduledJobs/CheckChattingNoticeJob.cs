using GMServer.Services;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;
using Quartz;
using NGEL.Data.Models;
using Newtonsoft.Json;

namespace GMServer.ScheduledJobs
{
    public class CheckChattingNoticeJob : IJob
    {
        private readonly ILogger<CheckChattingNoticeJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly StompService _stompService;
        private readonly ServerStateService _serverStateService;

        public CheckChattingNoticeJob(ILogger<CheckChattingNoticeJob> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment, StompService stompService, ServerStateService serverStateService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
            _stompService = stompService;
            _serverStateService = serverStateService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (false == _serverStateService.isMasterServer)
                return;

            int noticesCount = 0;
            var selectCurrentChattingNoticesTask = await _dbHelper.GameManager.SelectCurrentChattingNotices();
            if (selectCurrentChattingNoticesTask.Item1 && null != selectCurrentChattingNoticesTask.Item2 && 0 < selectCurrentChattingNoticesTask.Item2.Count)
                noticesCount = selectCurrentChattingNoticesTask.Item2.Count;

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[Start] CheckChattingNotice Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, notices count: {noticesCount}", ConsoleColor.Yellow);

            try
            {
                if (null != selectCurrentChattingNoticesTask.Item2 && 0 < selectCurrentChattingNoticesTask.Item2.Count)
                {
                    foreach (var notice in selectCurrentChattingNoticesTask.Item2)
                        _ = Notice(notice);
                }
                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[End] CheckChattingNotice Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, notices count: {noticesCount}", ConsoleColor.Yellow);
        }

        private async Task Notice(ChattingNotice notice)
        {
            if (1 > notice.visibleTime || 1 > notice.visibleCount)
                return;

            if (string.IsNullOrWhiteSpace(notice.message))
                return;

            var messageLines = notice.message.Split('\n')?.ToList() ?? new List<string>();
            if (null == messageLines || 1 > messageLines.Count)
                return;

            int repeatedCount = 0;
            int lineIndex = 0;

            var timer = new System.Timers.Timer();
            timer.Interval = notice.visibleTime * 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler((sender, e) => timerElapsed(notice, e));

            void timerElapsed(ChattingNotice? notice, System.Timers.ElapsedEventArgs e)
            {
                if (null == notice)
                {
                    timer.Dispose();
                    return;
                }

                if (repeatedCount >= ((ChattingNotice)notice).visibleCount)
                {
                    timer.Stop();
                    timer.Dispose();
                    return;
                }

                try
                {
                    var message = messageLines[lineIndex].Trim();
                    if (false == string.IsNullOrWhiteSpace(message))
                        _ = _stompService.PubNoticeAll(new ChattingNotice { message = message, noticeType = notice.noticeType, visibleTime = notice.visibleTime, visibleCount = 1 });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                }

                lineIndex++;
                if (messageLines.Count == lineIndex)
                {
                    lineIndex = 0;
                    repeatedCount++;
                }
            }

            lineIndex = 1;
            timer.Start();
            await _stompService.PubNoticeAll(new ChattingNotice { message = messageLines[0].Trim(), noticeType = notice.noticeType, visibleTime = notice.visibleTime, visibleCount = 1 });
        }
    }
}
