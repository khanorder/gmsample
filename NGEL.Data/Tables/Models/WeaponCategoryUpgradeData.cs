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
    public class WeaponCategoryUpgradeData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("WeaponCategoryID")]
        [Key("WeaponCategoryID")]
        public Int64 WeaponCategoryID { get; set; } = 0;
        [Name("Parameter")]
        [Key("Parameter")]
        public string Parameter { get; set; } = "";
        [Name("Value")]
        [Key("Value")]
        public float Value { get; set; } = 0;
        [Name("Rate")]
        [Key("Rate")]
        public float Rate { get; set; } = 0;
        [Name("Level")]
        [Key("Level")]
        public int Level { get; set; } = 0;
        [Name("Exp")]
        [Key("Exp")]
        public Int64 Exp { get; set; } = 0;
        [Name("AssetType")]
        [Key("AssetType")]
        public EUserAssetType AssetType { get; set; } = 0;
        [Name("AssetCount")]
        [Key("AssetCount")]
        public Int64 AssetCount { get; set; } = 0;
    }

    public class WeaponCategoryUpgradeDataMap : ClassMap<WeaponCategoryUpgradeData>
    {
        public WeaponCategoryUpgradeDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.WeaponCategoryID).Name("WeaponCategoryID");
            Map(m => m.Parameter).Name("Parameter");
            Map(m => m.Value).Name("Value");
            Map(m => m.Rate).Name("Rate");
            Map(m => m.Level).Name("Level");
            Map(m => m.Exp).Name("Exp");
            Map(m => m.AssetType).Name("AssetType");
            Map(m => m.AssetCount).Name("AssetCount");
        }
    }

}
