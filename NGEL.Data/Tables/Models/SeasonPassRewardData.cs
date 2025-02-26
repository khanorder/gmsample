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
    public class SeasonPassRewardData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("SeasonPassID")]
        [Key("SeasonPassID")]
        public int SeasonPassID { get; set; } = 0;
        [Name("SeasonPassLevel")]
        [Key("SeasonPassLevel")]
        public int SeasonPassLevel { get; set; } = 0;
        [Name("GroupIndex")]
        [Key("GroupIndex")]
        public int GroupIndex { get; set; } = 0;
        [Name("IsPaid")]
        [Key("IsPaid")]
        public bool IsPaid { get; set; } = false;
        [Name("RewardTableID")]
        [Key("RewardTableID")]
        public int RewardTableID { get; set; } = 0;
        [Name("IsMainReward")]
        [Key("IsMainReward")]
        public bool IsMainReward { get; set; } = false;
    }

    public class SeasonPassRewardDataMap : ClassMap<SeasonPassRewardData>
    {
        public SeasonPassRewardDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.SeasonPassID).Name("SeasonPassID");
            Map(m => m.SeasonPassLevel).Name("SeasonPassLevel");
            Map(m => m.GroupIndex).Name("GroupIndex");
            Map(m => m.IsPaid).Name("IsPaid");
            Map(m => m.RewardTableID).Name("RewardTableID");
            Map(m => m.IsMainReward).Name("IsMainReward");
        }
    }

}
