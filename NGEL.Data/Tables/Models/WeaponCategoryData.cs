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
    public class WeaponCategoryData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("WeaponCategory")]
        [Key("WeaponCategory")]
        public EWeaponCategory WeaponCategory { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
    }

    public class WeaponCategoryDataMap : ClassMap<WeaponCategoryData>
    {
        public WeaponCategoryDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.WeaponCategory).Name("WeaponCategory");
            Map(m => m.Name).Name("Name");
        }
    }

}
