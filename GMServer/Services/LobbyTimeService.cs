using Newtonsoft.Json;
using NGEL.Data;
using NGEL.Data.Settings;
using System;

namespace GMServer.Services
{
    public class LobbyTimeService
    {
        private readonly ILogger<LobbyTimeService> _logger;
        private readonly CommonSettings _commonSettings;
        private readonly Dictionary<string, DateTime> _lobbyTimes;

        public LobbyTimeService(ILogger<LobbyTimeService> logger, CommonSettings commonSettings)
        {
            _logger = logger;
            _commonSettings = commonSettings;
            _lobbyTimes = new Dictionary<string, DateTime>();
        }

        public void SetLobbyTime(string lobbyID, DateTime dateTime)
        {
            if (_lobbyTimes.TryGetValue(lobbyID, out var time))
            {
                _lobbyTimes[lobbyID] = dateTime;
            }
            else
            {
                _lobbyTimes.Add(lobbyID, dateTime);
            }

            if (Defines.ServerStateType.Service != _commonSettings.serverState)
                _logger.LogInformation(JsonConvert.SerializeObject(_lobbyTimes));
        }

        public Dictionary<string, DateTime> LobbyTimes { get => _lobbyTimes; }

        public DateTime? GetLobbyTime(string lobbyID)
        {
            if (false == _lobbyTimes.TryGetValue(lobbyID, out var time))
                return null;

            return time;
        }

    }
}
