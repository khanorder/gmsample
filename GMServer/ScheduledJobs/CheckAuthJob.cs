using Quartz;
using GMServer.Services;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;

namespace GMServer.ScheduledJobs
{
    public class CheckAuthJob : IJob
    {
        private readonly ILogger<CheckAuthJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        public CheckAuthJob(ILogger<CheckAuthJob> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //if (_environment.IsDevelopment())
            //    _logger.LogDebug($"[Start] CheckAuth Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, users count: {_gmAuthentication.GetSignedInUsers().Count}", ConsoleColor.Cyan);

            try
            {
                //if (0 < _gmAuthentication.GetSignedInUsers().Count)
                //{
                //    for (int i = 0; i < _gmAuthentication.GetSignedInUsers().Count; i++)
                //    {
                //        var user = _gmAuthentication.GetSignedInUsers()[i];
                //        if (null == user)
                //            continue;

                //        var diff = DateTime.UtcNow.Subtract(user.updatedTime);

                //        if (_environment.IsDevelopment())
                //            _logger.LogDebug($"[CheckSignIn] user: {user.name}, updateTime: {user.updatedTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff")}, diff: {string.Format(@"{0:#0.00}", diff.TotalMinutes)}", ConsoleColor.Cyan);

                //        if (diff.TotalMinutes > _commonSettings.authPersistence + 1)
                //        {
                //            if (_environment.IsDevelopment())
                //                _logger.LogInformation($"[DeleteSignIn] user: {user.name}, updateTime: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}", ConsoleColor.White, ConsoleColor.Red);

                //            await _gmAuthentication.RemoveSignInUser(user);
                //        }
                //    }
                //}
                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            //if (_environment.IsDevelopment())
            //    _logger.LogDebug($"[End] CheckAuth Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, users count: {_gmAuthentication.GetSignedInUsers().Count}", ConsoleColor.Cyan);
        }
    }
}
