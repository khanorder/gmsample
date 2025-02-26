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
    public class RewardData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("RewardType")]
        [Key("RewardType")]
        public string RewardType { get; set; } = "";
        [Name("RewardCount")]
        [Key("RewardCount")]
        public int RewardCount { get; set; } = 0;
        [Name("CharacterID")]
        [Key("CharacterID")]
        public Int64 CharacterID { get; set; } = 0;
        [Name("CostumeID1")]
        [Key("CostumeID1")]
        public Int64 CostumeID1 { get; set; } = 0;
        [Name("CostumeID2")]
        [Key("CostumeID2")]
        public Int64 CostumeID2 { get; set; } = 0;
        [Name("ItemID1")]
        [Key("ItemID1")]
        public Int64 ItemID1 { get; set; } = 0;
        [Name("ItemCount1")]
        [Key("ItemCount1")]
        public Int64 ItemCount1 { get; set; } = 0;
        [Name("ItemID2")]
        [Key("ItemID2")]
        public Int64 ItemID2 { get; set; } = 0;
        [Name("ItemCount2")]
        [Key("ItemCount2")]
        public Int64 ItemCount2 { get; set; } = 0;
        [Name("ItemID3")]
        [Key("ItemID3")]
        public Int64 ItemID3 { get; set; } = 0;
        [Name("ItemCount3")]
        [Key("ItemCount3")]
        public Int64 ItemCount3 { get; set; } = 0;
        [Name("AssetID1")]
        [Key("AssetID1")]
        public EUserAssetType AssetID1 { get; set; } = 0;
        [Name("AssetCount1")]
        [Key("AssetCount1")]
        public Int64 AssetCount1 { get; set; } = 0;
        [Name("AssetID2")]
        [Key("AssetID2")]
        public EUserAssetType AssetID2 { get; set; } = 0;
        [Name("AssetCount2")]
        [Key("AssetCount2")]
        public Int64 AssetCount2 { get; set; } = 0;
        [Name("AssetID3")]
        [Key("AssetID3")]
        public EUserAssetType AssetID3 { get; set; } = 0;
        [Name("AssetCount3")]
        [Key("AssetCount3")]
        public Int64 AssetCount3 { get; set; } = 0;
    }

    public class RewardDataMap : ClassMap<RewardData>
    {
        public RewardDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.RewardType).Name("RewardType");
            Map(m => m.RewardCount).Name("RewardCount");
            Map(m => m.CharacterID).Name("CharacterID");
            Map(m => m.CostumeID1).Name("CostumeID1");
            Map(m => m.CostumeID2).Name("CostumeID2");
            Map(m => m.ItemID1).Name("ItemID1");
            Map(m => m.ItemCount1).Name("ItemCount1");
            Map(m => m.ItemID2).Name("ItemID2");
            Map(m => m.ItemCount2).Name("ItemCount2");
            Map(m => m.ItemID3).Name("ItemID3");
            Map(m => m.ItemCount3).Name("ItemCount3");
            Map(m => m.AssetID1).Name("AssetID1");
            Map(m => m.AssetCount1).Name("AssetCount1");
            Map(m => m.AssetID2).Name("AssetID2");
            Map(m => m.AssetCount2).Name("AssetCount2");
            Map(m => m.AssetID3).Name("AssetID3");
            Map(m => m.AssetCount3).Name("AssetCount3");
        }
    }

}
