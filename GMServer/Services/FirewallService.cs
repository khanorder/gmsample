using Newtonsoft.Json;
using NGEL.Data.DB;
using NGEL.Data.Settings;

namespace GMServer.Services
{
    public class FirewallService
    {
        private readonly ILogger<FirewallService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;
        private readonly DBHelper _dbHelper;
        private List<string> _allowIPAddresses = new List<string>();
        private List<string> _denyIPAddresses = new List<string>();
        private object allowLockObject = new object();
        private object denyLockObject = new object();

        public FirewallService(ILogger<FirewallService> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings, DBHelper dbHelper)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _dbHelper = dbHelper;
        }

        public List<string> allowIPAddresses { get { return _allowIPAddresses; } }

        public List<string> denyIPAddresses { get { return _denyIPAddresses; } }

        public void setAllowIPAddresses (List<string> ipAddresses)
        {
            lock (allowLockObject)
            {
                _allowIPAddresses = new List<string> ();
                _allowIPAddresses.AddRange(ipAddresses);
            }
        }

        public void setDenyIPAddresses (List<string> ipAddresses)
        {
            lock (denyLockObject)
            {
                _denyIPAddresses = new List<string>();
                _denyIPAddresses.AddRange(ipAddresses);
            }
        }
    }
}
