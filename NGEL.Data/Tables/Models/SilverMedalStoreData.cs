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
    public class SilverMedalStoreData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("RewardType")]
        [Key("RewardType")]
        public ERewardType RewardType { get; set; } = 0;
        [Name("ItemType")]
        [Key("ItemType")]
        public EItemType ItemType { get; set; } = 0;
        [Name("ItemID")]
        [Key("ItemID")]
        public Int64 ItemID { get; set; } = 0;
        [Name("ItemCount")]
        [Key("ItemCount")]
        public Int64 ItemCount { get; set; } = 0;
        [Name("ReqAssetID")]
        [Key("ReqAssetID")]
        public EUserAssetType ReqAssetID { get; set; } = 0;
        [Name("ReqAssetCount")]
        [Key("ReqAssetCount")]
        public Int64 ReqAssetCount { get; set; } = 0;
    }

    public class SilverMedalStoreDataMap : ClassMap<SilverMedalStoreData>
    {
        public SilverMedalStoreDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.RewardType).Name("RewardType");
            Map(m => m.ItemType).Name("ItemType");
            Map(m => m.ItemID).Name("ItemID");
            Map(m => m.ItemCount).Name("ItemCount");
            Map(m => m.ReqAssetID).Name("ReqAssetID");
            Map(m => m.ReqAssetCount).Name("ReqAssetCount");
        }
    }

}
