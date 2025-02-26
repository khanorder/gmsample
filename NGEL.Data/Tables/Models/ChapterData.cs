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
    public class ChapterData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Chapter")]
        [Key("Chapter")]
        public Int64 Chapter { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("CharacterUnlock")]
        [Key("CharacterUnlock")]
        public List<Int64> CharacterUnlock { get; set; } = new List<Int64>();
        [Name("Rewards")]
        [Key("Rewards")]
        public List<Int64> Rewards { get; set; } = new List<Int64>();
        [Name("StageID")]
        [Key("StageID")]
        public List<Int64> StageID { get; set; } = new List<Int64>();
        [Name("StageGroup")]
        [Key("StageGroup")]
        public Int64 StageGroup { get; set; } = 0;
        [Name("NextChapterID")]
        [Key("NextChapterID")]
        public Int64 NextChapterID { get; set; } = 0;
    }

    public class ChapterDataMap : ClassMap<ChapterData>
    {
        public ChapterDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Chapter).Name("Chapter");
            Map(m => m.Name).Name("Name");
            Map(m => m.CharacterUnlock).Name("CharacterUnlock");
            Map(m => m.Rewards).Name("Rewards");
            Map(m => m.StageID).Name("StageID");
            Map(m => m.StageGroup).Name("StageGroup");
            Map(m => m.NextChapterID).Name("NextChapterID");
        }
    }

}
