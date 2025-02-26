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
    public class IntroduceData
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
        [Name("IntroduceCategory")]
        [Key("IntroduceCategory")]
        public string IntroduceCategory { get; set; } = "";
        [Name("AchievementID")]
        [Key("AchievementID")]
        public int AchievementID { get; set; } = 0;
    }

    public class IntroduceDataMap : ClassMap<IntroduceData>
    {
        public IntroduceDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.IntroduceCategory).Name("IntroduceCategory");
            Map(m => m.AchievementID).Name("AchievementID");
        }
    }

}
