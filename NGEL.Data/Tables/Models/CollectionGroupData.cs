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
    public class CollectionGroupData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("UIIdentifier")]
        [Key("UIIdentifier")]
        public string UIIdentifier { get; set; } = "";
        [Name("CategoryName")]
        [Key("CategoryName")]
        public string CategoryName { get; set; } = "";
        [Key("CategoryNameString")]
        public string CategoryNameString { get; set; } = "";
        [Key("CategoryNameStringWithID")]
        public string CategoryNameStringWithID { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
    }

    public class CollectionGroupDataMap : ClassMap<CollectionGroupData>
    {
        public CollectionGroupDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.UIIdentifier).Name("UIIdentifier");
            Map(m => m.CategoryName).Name("CategoryName");
            Map(m => m.Enable).Name("Enable");
        }
    }

}
