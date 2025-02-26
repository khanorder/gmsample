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
    public class BattleStoreData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("MainType")]
        [Key("MainType")]
        public string MainType { get; set; } = "";
        [Name("SubType")]
        [Key("SubType")]
        public string SubType { get; set; } = "";
        [Name("SellItem")]
        [Key("SellItem")]
        public Int64 SellItem { get; set; } = 0;
        [Name("SellAsset")]
        [Key("SellAsset")]
        public string SellAsset { get; set; } = "";
        [Name("Count")]
        [Key("Count")]
        public Int64 Count { get; set; } = 0;
        [Name("CostAssetType")]
        [Key("CostAssetType")]
        public string CostAssetType { get; set; } = "";
        [Name("CostCount")]
        [Key("CostCount")]
        public Int64 CostCount { get; set; } = 0;
    }

    public class BattleStoreDataMap : ClassMap<BattleStoreData>
    {
        public BattleStoreDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.MainType).Name("MainType");
            Map(m => m.SubType).Name("SubType");
            Map(m => m.SellItem).Name("SellItem");
            Map(m => m.SellAsset).Name("SellAsset");
            Map(m => m.Count).Name("Count");
            Map(m => m.CostAssetType).Name("CostAssetType");
            Map(m => m.CostCount).Name("CostCount");
        }
    }

}
