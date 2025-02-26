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
    public class InstantGuideUIData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("GuideCategory")]
        [Key("GuideCategory")]
        public int GuideCategory { get; set; } = 0;
        [Name("GuideCategoryName")]
        [Key("GuideCategoryName")]
        public string GuideCategoryName { get; set; } = "";
        [Key("GuideCategoryNameString")]
        public string GuideCategoryNameString { get; set; } = "";
        [Key("GuideCategoryNameStringWithID")]
        public string GuideCategoryNameStringWithID { get; set; } = "";
        [Name("Title")]
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("TitleString")]
        public string TitleString { get; set; } = "";
        [Key("TitleStringWithID")]
        public string TitleStringWithID { get; set; } = "";
    }

    public class InstantGuideUIDataMap : ClassMap<InstantGuideUIData>
    {
        public InstantGuideUIDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.GuideCategory).Name("GuideCategory");
            Map(m => m.GuideCategoryName).Name("GuideCategoryName");
            Map(m => m.Title).Name("Title");
        }
    }

}
