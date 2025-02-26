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
    public class AchievementData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("AchievementGroupID")]
        [Key("AchievementGroupID")]
        public Int64 AchievementGroupID { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("AchievementType")]
        [Key("AchievementType")]
        public string AchievementType { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("AchievementCount")]
        [Key("AchievementCount")]
        public Int64 AchievementCount { get; set; } = 0;
        [Name("RewardAssetType")]
        [Key("RewardAssetType")]
        public EUserAssetType RewardAssetType { get; set; } = 0;
        [Name("RewardAssetCount")]
        [Key("RewardAssetCount")]
        public Int64 RewardAssetCount { get; set; } = 0;
    }

    public class AchievementDataMap : ClassMap<AchievementData>
    {
        public AchievementDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.AchievementGroupID).Name("AchievementGroupID");
            Map(m => m.Name).Name("Name");
            Map(m => m.AchievementType).Name("AchievementType");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.AchievementCount).Name("AchievementCount");
            Map(m => m.RewardAssetType).Name("RewardAssetType");
            Map(m => m.RewardAssetCount).Name("RewardAssetCount");
        }
    }

}
