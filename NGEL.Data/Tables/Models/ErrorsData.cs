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
    public class ErrorsData
    {
        [Name("Error")]
        [Key("Error")]
        public Errors Error { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
    }

    public class ErrorsDataMap : ClassMap<ErrorsData>
    {
        public ErrorsDataMap()
        {
            Map(m => m.Error).Name("Error");
            Map(m => m.Name).Name("Name");
        }
    }

}
