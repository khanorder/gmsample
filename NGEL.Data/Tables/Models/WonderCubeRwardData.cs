/********************************************************/
/*Auto Create File*/
/*Source : Tool.AutoCreateComponents*/
/********************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Lobby;

namespace NGEL.Data.Tables.Models
{

    [MessagePackObject]
    public class WonderCubeRwardData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("RewardGroup")]
        [Key("RewardGroup")]
        public int RewardGroup { get; set; } = 0;
        [Name("RewardType")]
        [Key("RewardType")]
        public ERewardType RewardType { get; set; } = 0;
        [Name("RewardID")]
        [Key("RewardID")]
        public Int64 RewardID { get; set; } = 0;
        [Name("RewardCount")]
        [Key("RewardCount")]
        public Int64 RewardCount { get; set; } = 0;
        [Name("RewardProb")]
        [Key("RewardProb")]
        public Int64 RewardProb { get; set; } = 0;
    }

    public class WonderCubeRwardDataMap : ClassMap<WonderCubeRwardData>
    {
        public WonderCubeRwardDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.RewardGroup).Name("RewardGroup");
            Map(m => m.RewardType).Name("RewardType");
            Map(m => m.RewardID).Name("RewardID");
            Map(m => m.RewardCount).Name("RewardCount");
            Map(m => m.RewardProb).Name("RewardProb");
        }
    }

}
