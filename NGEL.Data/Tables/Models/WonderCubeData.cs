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
    public class WonderCubeData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("SlotCount")]
        [Key("SlotCount")]
        public int SlotCount { get; set; } = 0;
        [Name("AssetType1")]
        [Key("AssetType1")]
        public EUserAssetType AssetType1 { get; set; } = 0;
        [Name("AssetCount1")]
        [Key("AssetCount1")]
        public List<Int64> AssetCount1 { get; set; } = new List<Int64>();
        [Name("AssetType2")]
        [Key("AssetType2")]
        public EUserAssetType AssetType2 { get; set; } = 0;
        [Name("AssetCount2")]
        [Key("AssetCount2")]
        public List<Int64> AssetCount2 { get; set; } = new List<Int64>();
    }

    public class WonderCubeDataMap : ClassMap<WonderCubeData>
    {
        public WonderCubeDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.SlotCount).Name("SlotCount");
            Map(m => m.AssetType1).Name("AssetType1");
            Map(m => m.AssetCount1).Name("AssetCount1");
            Map(m => m.AssetType2).Name("AssetType2");
            Map(m => m.AssetCount2).Name("AssetCount2");
        }
    }

}
