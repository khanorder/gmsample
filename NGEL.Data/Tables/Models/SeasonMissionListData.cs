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
    public class SeasonMissionListData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("MissionGroup")]
        [Key("MissionGroup")]
        public EMissionGroup MissionGroup { get; set; } = 0;
        [Name("IsTargetName")]
        [Key("IsTargetName")]
        public bool IsTargetName { get; set; } = false;
        [Name("ReqHeroID")]
        [Key("ReqHeroID")]
        public int ReqHeroID { get; set; } = 0;
        [Key("CharacterName")]
        public string CharacterName { get; set; } = "";
        [Key("CharacterNameString")]
        public string CharacterNameString { get; set; } = "";
        [Key("CharacterNameStringWithID")]
        public string CharacterNameStringWithID { get; set; } = "";
        [Name("MissionType")]
        [Key("MissionType")]
        public EMissionType MissionType { get; set; } = 0;
        [Name("MissionValue")]
        [Key("MissionValue")]
        public int MissionValue { get; set; } = 0;
        [Name("MissionCount")]
        [Key("MissionCount")]
        public int MissionCount { get; set; } = 0;
        [Name("Description")]
        [Key("Description")]
        public string Description { get; set; } = "";
        [Key("DescriptionString")]
        public string DescriptionString { get; set; } = "";
        [Key("DescriptionStringWithID")]
        public string DescriptionStringWithID { get; set; } = "";
        [Name("RewardExp")]
        [Key("RewardExp")]
        public Int64 RewardExp { get; set; } = 0;
    }

    public class SeasonMissionListDataMap : ClassMap<SeasonMissionListData>
    {
        public SeasonMissionListDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.MissionGroup).Name("MissionGroup");
            Map(m => m.IsTargetName).Name("IsTargetName");
            Map(m => m.ReqHeroID).Name("ReqHeroID");
            Map(m => m.MissionType).Name("MissionType");
            Map(m => m.MissionValue).Name("MissionValue");
            Map(m => m.MissionCount).Name("MissionCount");
            Map(m => m.Description).Name("Description");
            Map(m => m.RewardExp).Name("RewardExp");
        }
    }

}
