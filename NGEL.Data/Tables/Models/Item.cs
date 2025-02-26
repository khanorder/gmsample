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
    public class Item
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
        [Name("ItemType")]
        [Key("ItemType")]
        public EItemType ItemType { get; set; } = 0;
        [Name("WeaponType")]
        [Key("WeaponType")]
        public string WeaponType { get; set; } = "";
        [Name("Grade")]
        [Key("Grade")]
        public EItemGradeType Grade { get; set; } = 0;
    }

    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.ItemType).Name("ItemType");
            Map(m => m.WeaponType).Name("WeaponType");
            Map(m => m.Grade).Name("Grade");
        }
    }

}
