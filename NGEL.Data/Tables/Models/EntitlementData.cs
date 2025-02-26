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
    public class EntitlementData
    {
        [Name("ID")]
        [Key("ID")]
        public string ID { get; set; } = "";
        [Name("EntitlementName")]
        [Key("EntitlementName")]
        public string EntitlementName { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
    }

    public class EntitlementDataMap : ClassMap<EntitlementData>
    {
        public EntitlementDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.EntitlementName).Name("EntitlementName");
            Map(m => m.Enable).Name("Enable");
        }
    }

}
