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
    public class StringData
    {
        [Name("Key")]
        [Key("Key")]
        public string Key { get; set; } = "";
        [Name("SourceString")]
        [Key("SourceString")]
        public string SourceString { get; set; } = "";
    }

    public class StringDataMap : ClassMap<StringData>
    {
        public StringDataMap()
        {
            Map(m => m.Key).Name("Key");
            Map(m => m.SourceString).Name("SourceString");
        }
    }

}
