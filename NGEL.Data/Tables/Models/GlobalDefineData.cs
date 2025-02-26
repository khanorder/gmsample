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
    public class GlobalDefineData
    {
        [Name("ID")]
        [Key("ID")]
        public string ID { get; set; } = "";
        [Name("Value")]
        [Key("Value")]
        public List<string> Value { get; set; } = new List<string>();
    }

    public class GlobalDefineDataMap : ClassMap<GlobalDefineData>
    {
        public GlobalDefineDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Value).Name("Value");
        }
    }

}
