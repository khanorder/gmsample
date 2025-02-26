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
    public class IncubationData
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
    }

    public class IncubationDataMap : ClassMap<IncubationData>
    {
        public IncubationDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
        }
    }

}
