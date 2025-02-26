using GMServer.Services;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Helpers;
using Lobby;
using Quartz;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace GMServer.ScheduledJobs
{
    public class CheckFirewallJob : IJob
    {
        private readonly ILogger<CheckFirewallJob> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly StompService _stompService;
        private readonly FirewallService _firewallService;
        private readonly ServerStateService _serverStateService;

        public CheckFirewallJob(ILogger<CheckFirewallJob> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment, StompService stompService, FirewallService firewallService, ServerStateService serverStateService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
            _stompService = stompService;
            _firewallService = firewallService;
            _serverStateService = serverStateService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (false == _serverStateService.isMasterServer)
                return;

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[Start] CheckFirewallJob Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, allowedCount: {_firewallService.allowIPAddresses.Count}, deniedCount: {_firewallService.denyIPAddresses.Count}", ConsoleColor.Yellow);

            try
            {
                var selectFirewallsTask = await _dbHelper.GameManager.SelectFirewalls();
                if (selectFirewallsTask.Item1 && null != selectFirewallsTask.Item2 && 0 < selectFirewallsTask.Item2.Count)
                {
                    var allowIPAddreses = new List<string>();
                    var denyIPAddreses = new List<string>();

                    foreach (var firewall in selectFirewallsTask.Item2)
                    {
                        switch (firewall.method)
                        {
                            case Defines.FirewallMethod.Allow:
                                allowIPAddreses.Add(firewall.ipAddress);
                                break;

                            case Defines.FirewallMethod.Deny:
                                denyIPAddreses.Add(firewall.ipAddress);
                                break;
                        }
                    }

                    _firewallService.setAllowIPAddresses(allowIPAddreses);
                    _firewallService.setDenyIPAddresses(denyIPAddreses);
                }

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }

            if (_environment.IsDevelopment())
                _logger.LogInformation($"[End] CheckFirewallJob Job: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}, allowedCount: {_firewallService.allowIPAddresses.Count}, deniedCount: {_firewallService.denyIPAddresses.Count}", ConsoleColor.Yellow);
        }
    }
}
