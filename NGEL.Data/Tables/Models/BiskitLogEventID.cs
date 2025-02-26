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
    public class BiskitLogEventID
    {
        [Name("SeqID")]
        [Key("SeqID")]
        public Int64 SeqID { get; set; } = 0;
        [Name("EventID")]
        [Key("EventID")]
        public string EventID { get; set; } = "";
        [Name("EventName")]
        [Key("EventName")]
        public string EventName { get; set; } = "";
    }

    public class BiskitLogEventIDMap : ClassMap<BiskitLogEventID>
    {
        public BiskitLogEventIDMap()
        {
            Map(m => m.SeqID).Name("SeqID");
            Map(m => m.EventID).Name("EventID");
            Map(m => m.EventName).Name("EventName");
        }
    }

}
