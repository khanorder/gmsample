using NGEL.Data.DB;
using NGEL.Data.Settings;

namespace GMServer.Services
{
    public class ServerStateService
    {
        private readonly ILogger<ServerStateService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;
        private readonly DBHelper _dbHelper;
        private readonly Guid _serverId;
        private readonly int _masterVersion;
        private readonly int _updateVersion;
        private readonly int _maintenanceVersion;
        private readonly int _recommendClientMasterVersion;
        private readonly int _recommendClientUpdateVersion;
        private readonly int _recommendClientMaintenanceVersion;
        private bool _isMasterServer;

        public ServerStateService(ILogger<ServerStateService> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings, DBHelper dbHelper)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _dbHelper = dbHelper;
            _serverId = Guid.NewGuid();
            _masterVersion = 0;
            _updateVersion = 240228;
            _maintenanceVersion = 3;
            _recommendClientMasterVersion = 0;
            _recommendClientUpdateVersion = 240228;
            _recommendClientMaintenanceVersion = 6;
            _isMasterServer = false;
        }

        public Guid serverId { get => _serverId; }
        public string version { get => $"{_masterVersion}.{_updateVersion}.{_maintenanceVersion}"; }
        public string recommendClientVersion { get => $"{_recommendClientMasterVersion}.{_recommendClientUpdateVersion}.{_recommendClientMaintenanceVersion}"; }
        public int masterVersion { get => _masterVersion; }
        public int updateVersion { get => _updateVersion; }
        public int maintenanceVersion { get => _maintenanceVersion; }
        public int recommendClientMasterVersion { get => _recommendClientMasterVersion; }
        public int recommendClientUpdateVersion { get => _recommendClientUpdateVersion; }
        public int recommendClientMaintenanceVersion { get => _recommendClientMaintenanceVersion; }
        public bool isMasterServer { get => _isMasterServer; }

        public void SetMasterServer ()
        {
            _isMasterServer = true;
        }

        public void ClearMasterServer()
        {
            _isMasterServer = false;
        }
    }
}
