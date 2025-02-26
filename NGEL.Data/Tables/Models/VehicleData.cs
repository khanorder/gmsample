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
    public class VehicleData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Description")]
        [Key("Description")]
        public string Description { get; set; } = "";
        [Key("DescriptionString")]
        public string DescriptionString { get; set; } = "";
        [Key("DescriptionStringWithID")]
        public string DescriptionStringWithID { get; set; } = "";
        [Name("Type")]
        [Key("Type")]
        public string Type { get; set; } = "";
    }

    public class VehicleDataMap : ClassMap<VehicleData>
    {
        public VehicleDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Description).Name("Description");
            Map(m => m.Type).Name("Type");
        }
    }

}
