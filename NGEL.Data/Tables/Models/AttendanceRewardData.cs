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
    public class AttendanceRewardData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("RewardGroup")]
        [Key("RewardGroup")]
        public int RewardGroup { get; set; } = 0;
        [Name("GroupIndex")]
        [Key("GroupIndex")]
        public int GroupIndex { get; set; } = 0;
        [Name("AssetType")]
        [Key("AssetType")]
        public EUserAssetType AssetType { get; set; } = 0;
        [Name("AssetCount")]
        [Key("AssetCount")]
        public Int64 AssetCount { get; set; } = 0;
        [Name("ItemID")]
        [Key("ItemID")]
        public int ItemID { get; set; } = 0;
        [Name("ItemCount")]
        [Key("ItemCount")]
        public Int64 ItemCount { get; set; } = 0;
        [Name("HeroID")]
        [Key("HeroID")]
        public int HeroID { get; set; } = 0;
    }

    public class AttendanceRewardDataMap : ClassMap<AttendanceRewardData>
    {
        public AttendanceRewardDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.RewardGroup).Name("RewardGroup");
            Map(m => m.GroupIndex).Name("GroupIndex");
            Map(m => m.AssetType).Name("AssetType");
            Map(m => m.AssetCount).Name("AssetCount");
            Map(m => m.ItemID).Name("ItemID");
            Map(m => m.ItemCount).Name("ItemCount");
            Map(m => m.HeroID).Name("HeroID");
        }
    }

}
