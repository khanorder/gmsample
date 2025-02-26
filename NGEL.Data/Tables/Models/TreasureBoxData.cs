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
    public class TreasureBoxData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("AssetType")]
        [Key("AssetType")]
        public List<EUserAssetType> AssetType { get; set; } = new List<EUserAssetType>();
        [Name("AssetCount")]
        [Key("AssetCount")]
        public List<Int64> AssetCount { get; set; } = new List<Int64>();
        [Name("RewardItemID")]
        [Key("RewardItemID")]
        public List<Int64> RewardItemID { get; set; } = new List<Int64>();
        [Name("RewardItemCount")]
        [Key("RewardItemCount")]
        public List<Int64> RewardItemCount { get; set; } = new List<Int64>();
        [Name("Location")]
        [Key("Location")]
        public string Location { get; set; } = "";
        [Name("Rotation")]
        [Key("Rotation")]
        public string Rotation { get; set; } = "";
        [Name("Scale")]
        [Key("Scale")]
        public string Scale { get; set; } = "";
    }

    public class TreasureBoxDataMap : ClassMap<TreasureBoxData>
    {
        public TreasureBoxDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.AssetType).Name("AssetType");
            Map(m => m.AssetCount).Name("AssetCount");
            Map(m => m.RewardItemID).Name("RewardItemID");
            Map(m => m.RewardItemCount).Name("RewardItemCount");
            Map(m => m.Location).Name("Location");
            Map(m => m.Rotation).Name("Rotation");
            Map(m => m.Scale).Name("Scale");
        }
    }

}
