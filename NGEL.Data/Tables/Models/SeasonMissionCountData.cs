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
    public class SeasonMissionCountData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("OpenWorldCount")]
        [Key("OpenWorldCount")]
        public int OpenWorldCount { get; set; } = 0;
        [Name("GoldClashCount")]
        [Key("GoldClashCount")]
        public int GoldClashCount { get; set; } = 0;
        [Name("RPGCount")]
        [Key("RPGCount")]
        public int RPGCount { get; set; } = 0;
    }

    public class SeasonMissionCountDataMap : ClassMap<SeasonMissionCountData>
    {
        public SeasonMissionCountDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.OpenWorldCount).Name("OpenWorldCount");
            Map(m => m.GoldClashCount).Name("GoldClashCount");
            Map(m => m.RPGCount).Name("RPGCount");
        }
    }

}
