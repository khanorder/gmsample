using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NGEL.Data.Models;
using NGEL.Data.Settings;
using NGEL.Data.Tables;
using StackExchange.Redis;

namespace NGEL.Data.DB
{
    public class DBHelperActiveUser
    {
        private readonly CommonSettings _commonSettings;
        private readonly DBHelperGame _gameDB;
        private readonly ILogger<DBHelperActiveUser> _logger;
        private readonly DataTableService _dataTableService;
        private string _conStr { get; set; } = "";

        public DBHelperActiveUser(CommonSettings commonSettings, DBHelperGame gameDB, ILogger<DBHelperActiveUser> logger, DataTableService dataTableService)
        {
            _commonSettings = commonSettings;
            _gameDB = gameDB;
            _logger = logger;
            _dataTableService = dataTableService;
        }

        public void InitDBConnect(string connectionString)
        {
            _conStr = connectionString;
        }

        public async Task<ActiveUserAccount?> GetUserAccountInfo(Int64 UID)
        {
            ActiveUserAccount? result = null;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                var redisValue = (await db.StringGetAsync($"Account:UID:{UID}"));
                if (false == string.IsNullOrWhiteSpace(redisValue.ToString()))
                    result = JsonConvert.DeserializeObject<ActiveUserAccount>(redisValue.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<(bool, string)> IsUserInLobby(Int64 UID)
        {
            var result = (false, "");
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                ActiveUser? activeUser = null;
                var redisValue = (await db.HashGetAsync("ActiveUser", UID));
                if (false == string.IsNullOrWhiteSpace(redisValue.ToString()))
                    activeUser = JsonConvert.DeserializeObject<ActiveUser>(redisValue.ToString());

                if (null == activeUser || string.IsNullOrWhiteSpace(activeUser.LobbyID))
                    return (false, "");

                result = (true, activeUser.LobbyID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<Int64> GetMaxMailID()
        {
            Int64 result = 0;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                var redisValue = (await db.HashIncrementAsync("UniqueID", "Mail"));

                result = redisValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<OpenWorldTime> GetOpenWorldTime()
        {
            var result = new OpenWorldTime();
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                var redisValue = (await db.StringGetAsync("OpenWorldTime"));
                if (false == string.IsNullOrWhiteSpace(redisValue.ToString()))
                {
                    var openWorldTime = JsonConvert.DeserializeObject<OpenWorldTime>(redisValue.ToString());
                    if (null != openWorldTime)
                        result = openWorldTime;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<bool> SetOpenWorldTime(OpenWorldTime openWorldTime)
        {
            var result = false;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                result = await db.StringSetAsync("OpenWorldTime", JsonConvert.SerializeObject(openWorldTime, Formatting.Indented));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<GoldClashTime> GetGoldClashTime()
        {
            var result = new GoldClashTime();
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                var redisValue = (await db.StringGetAsync("GoldClashOpenTime"));
                if (false == string.IsNullOrWhiteSpace(redisValue.ToString()))
                {
                    var goldClashTime = JsonConvert.DeserializeObject<GoldClashTime>(redisValue.ToString());
                    if (null != goldClashTime)
                        result = goldClashTime;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<bool> SetGoldClashTime(GoldClashTime goldClashTime)
        {
            var result = false;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                result = await db.StringSetAsync("GoldClashOpenTime", JsonConvert.SerializeObject(goldClashTime, Formatting.Indented));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<List<GameRankingScore>> GetRankingGame(Defines.GameType gameName)
        {
            return await GetRankingGame(gameName, DateTime.Now);
        }

        public async Task<List<GameRankingScore>> GetRankingGame(Defines.GameType gameName, DateTime dateTime)
        {
            var result = new List<GameRankingScore>();
            List<Int64> UIDList = new List<Int64>();
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase(1);
                int pageSize = 100;
                var totalCount = db.SortedSetLength($"Ranking:{gameName.ToString()}:{dateTime.ToString("yyyy_MM_dd")}");
                Int64 offsetCount = 0;
                if (totalCount > pageSize)
                {
                    var blockCount = (int)Math.Floor((decimal)totalCount / pageSize);
                    offsetCount = totalCount - (pageSize * blockCount);
                }

                var keyEnd = dateTime.ToString("yyyy_MM_dd");
                if (Defines.GameType.GoldClash == gameName)
                {
                    var seasonPassData = _dataTableService._seasonPassDataTableService.datas.Find(_ => _.StartDate <= dateTime && _.EndDate >= dateTime);
                    if (null == seasonPassData)
                        return result;

                    keyEnd = seasonPassData.SeasonNum.ToString();
                }
                var selectRankingData = db.SortedSetRangeByScoreWithScores($"Ranking:{gameName.ToString()}:{keyEnd}", order: Order.Descending, take: pageSize, skip: offsetCount); ;
                
                //var selectRankingData = db.SortedSetRangeByScore($"Ranking:{gameName.ToString()}:{dateTime.ToString("yyyy_MM_dd")}", order: Order.Descending, skip: offsetCount, take: pageSize);
                if (null != selectRankingData && 0 < selectRankingData.Count())
                {
                    var entries = selectRankingData.ToList();
                    foreach (var entry in entries)
                    {
                        try
                        {
                            var UID = Int64.Parse(entry.Element.ToString());
                            UIDList.Add(UID);
                            result.Insert(0, new GameRankingScore { UID = UID, Score = entry.Score });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            _logger.LogError(ex.StackTrace);
                        }
                    }

                    var selectUserAccountsTask = await _gameDB.SelectUserAccounts(string.Join(",", UIDList));
                    if (selectUserAccountsTask.Item1 && 0 < selectUserAccountsTask.Item2.Count)
                    {
                        foreach (var rankingData in result)
                        {
                            var userAccount = selectUserAccountsTask.Item2.Find(_ => _.UID == rankingData.UID);
                            if (null != userAccount)
                                rankingData.UserAccount = userAccount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<List<WhiteListData>> GetWhiteList()
        {
            var result = new List<WhiteListData>();
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                var whiteListDatas = db.SetScan($"WhiteList");
                if (null != whiteListDatas && 0 < whiteListDatas.Count())
                {
                    foreach (var ip in whiteListDatas)
                    {
                        result.Add(new WhiteListData { whiteIP = ip.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<bool> AddWhiteList(string whiteIP)
        {
            var result = false;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                result = await db.SetAddAsync("WhiteList", whiteIP);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }

        public async Task<bool> RemoveWhiteList(string whiteIP)
        {
            var result = false;
            ConnectionMultiplexer? connection = null;
            try
            {
                connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { _conStr } });
                var db = connection.GetDatabase();
                result = await db.SetRemoveAsync("WhiteList", whiteIP);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (null != connection)
                    await connection.DisposeAsync();
            }
            return result;
        }
    }
}