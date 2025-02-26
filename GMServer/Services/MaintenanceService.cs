using NGEL.Data.DB;
using NGEL.Data.Models;
using NGEL.Data.Settings;

namespace GMServer.Services
{
    public class MaintenanceService
    {
        private readonly ILogger<MaintenanceService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;
        private List<Maintenance> _maintenances;
        private DBHelper _dbHelper;
        private object lockObject = new object();

        public MaintenanceService(ILogger<MaintenanceService> logger, IHttpContextAccessor httpContextAccessor, CommonSettings commonSettings, DBHelper dbHelper)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _dbHelper = dbHelper;
            _maintenances = new List<Maintenance>();
        }

        public List<Maintenance> maintenances { get { return _maintenances; } }

        public async Task Init()
        {
            _maintenances.Clear();
            var selectMaintenacesTask = await _dbHelper.Admin.SelectMaintenances();
            if (selectMaintenacesTask.Item1 && 0 < selectMaintenacesTask.Item2.Count)
                _maintenances = selectMaintenacesTask.Item2;
        }

        public void AddMaintenance(Maintenance data)
        {
            var existsData = _maintenances.Find(_ => _.Platform == data.Platform && _.Area == data.Area);
            lock (lockObject)
            {
                if (null != existsData)
                    _maintenances.Remove(existsData);
                _maintenances.Add(data);
            }
        }

        public void RemoveMaintenance(Maintenance data)
        {
            var existsData = _maintenances.Find(_ => _.Platform == data.Platform && _.Area == data.Area);
            if (null != existsData)
            {
                lock (lockObject)
                {
                    _maintenances.Remove(existsData);
                }
            }
        }

        public void CheckKick(Maintenance data)
        {
            var existsData = _maintenances.Find(_ => _.Platform == data.Platform && _.Area == data.Area);
            if (null != existsData)
            {
                lock (lockObject)
                {
                    existsData.isKicked = true;
                }
            }
        }
    }
}
