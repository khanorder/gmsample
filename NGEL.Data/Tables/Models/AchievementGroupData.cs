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
    public class AchievementGroupData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("GameMode")]
        [Key("GameMode")]
        public EContentsType GameMode { get; set; } = 0;
        [Name("ChapterID")]
        [Key("ChapterID")]
        public Int64 ChapterID { get; set; } = 0;
        [Name("CategoryName")]
        [Key("CategoryName")]
        public string CategoryName { get; set; } = "";
        [Key("CategoryNameString")]
        public string CategoryNameString { get; set; } = "";
        [Key("CategoryNameStringWithID")]
        public string CategoryNameStringWithID { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("EntitlementID")]
        [Key("EntitlementID")]
        public Int64 EntitlementID { get; set; } = 0;
    }

    public class AchievementGroupDataMap : ClassMap<AchievementGroupData>
    {
        public AchievementGroupDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.GameMode).Name("GameMode");
            Map(m => m.ChapterID).Name("ChapterID");
            Map(m => m.CategoryName).Name("CategoryName");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.EntitlementID).Name("EntitlementID");
        }
    }

}
