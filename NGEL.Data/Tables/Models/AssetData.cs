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
    public class AssetData
    {
        [Name("ID")]
        [Key("ID")]
        public EUserAssetType ID { get; set; } = 0;
        [Name("ItemID")]
        [Key("ItemID")]
        public Int64 ItemID { get; set; } = 0;
        [Name("MaxValue")]
        [Key("MaxValue")]
        public Int64 MaxValue { get; set; } = 0;
        [Name("AssetString")]
        [Key("AssetString")]
        public string AssetString { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
    }

    public class AssetDataMap : ClassMap<AssetData>
    {
        public AssetDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.ItemID).Name("ItemID");
            Map(m => m.MaxValue).Name("MaxValue");
            Map(m => m.AssetString).Name("AssetString");
        }
    }

}
