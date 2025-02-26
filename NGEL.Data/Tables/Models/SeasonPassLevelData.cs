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
    public class SeasonPassLevelData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("SeasonPassLevel")]
        [Key("SeasonPassLevel")]
        public int SeasonPassLevel { get; set; } = 0;
        [Name("SeasonPassID")]
        [Key("SeasonPassID")]
        public int SeasonPassID { get; set; } = 0;
        [Name("ReqExp")]
        [Key("ReqExp")]
        public int ReqExp { get; set; } = 0;
    }

    public class SeasonPassLevelDataMap : ClassMap<SeasonPassLevelData>
    {
        public SeasonPassLevelDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.SeasonPassLevel).Name("SeasonPassLevel");
            Map(m => m.SeasonPassID).Name("SeasonPassID");
            Map(m => m.ReqExp).Name("ReqExp");
        }
    }

}
