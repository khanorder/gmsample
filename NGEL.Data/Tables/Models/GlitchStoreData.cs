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
    public class GlitchStoreData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("ProductType")]
        [Key("ProductType")]
        public string ProductType { get; set; } = "";
        [Name("ProductItem")]
        [Key("ProductItem")]
        public Int64 ProductItem { get; set; } = 0;
        [Name("AssetType")]
        [Key("AssetType")]
        public string AssetType { get; set; } = "";
        [Name("AssetPrice")]
        [Key("AssetPrice")]
        public Int64 AssetPrice { get; set; } = 0;
        [Name("Chapter")]
        [Key("Chapter")]
        public int Chapter { get; set; } = 0;
    }

    public class GlitchStoreDataMap : ClassMap<GlitchStoreData>
    {
        public GlitchStoreDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.ProductType).Name("ProductType");
            Map(m => m.ProductItem).Name("ProductItem");
            Map(m => m.AssetType).Name("AssetType");
            Map(m => m.AssetPrice).Name("AssetPrice");
            Map(m => m.Chapter).Name("Chapter");
        }
    }

}
