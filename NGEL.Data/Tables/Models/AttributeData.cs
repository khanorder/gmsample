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
    public class AttributeData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("MaxLevel")]
        [Key("MaxLevel")]
        public int MaxLevel { get; set; } = 0;
    }

    public class AttributeDataMap : ClassMap<AttributeData>
    {
        public AttributeDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.MaxLevel).Name("MaxLevel");
        }
    }

}
