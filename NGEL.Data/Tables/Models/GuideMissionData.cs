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
    public class GuideMissionData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("GuideMissionCategory")]
        [Key("GuideMissionCategory")]
        public EGuideMissionCategory GuideMissionCategory { get; set; } = 0;
        [Name("MissionType")]
        [Key("MissionType")]
        public EGuideMissionType MissionType { get; set; } = 0;
        [Name("Step")]
        [Key("Step")]
        public int Step { get; set; } = 0;
        [Name("NextStepMissionID")]
        [Key("NextStepMissionID")]
        public Int64 NextStepMissionID { get; set; } = 0;
        [Name("Title")]
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("TitleString")]
        public string TitleString { get; set; } = "";
        [Key("TitleStringWithID")]
        public string TitleStringWithID { get; set; } = "";
        [Name("TrackingID")]
        [Key("TrackingID")]
        public Int64 TrackingID { get; set; } = 0;
        [Name("RewardType")]
        [Key("RewardType")]
        public ERewardType RewardType { get; set; } = 0;
        [Name("RewardID")]
        [Key("RewardID")]
        public string RewardID { get; set; } = "";
        [Name("RewardCount")]
        [Key("RewardCount")]
        public Int64 RewardCount { get; set; } = 0;
    }

    public class GuideMissionDataMap : ClassMap<GuideMissionData>
    {
        public GuideMissionDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.GuideMissionCategory).Name("GuideMissionCategory");
            Map(m => m.MissionType).Name("MissionType");
            Map(m => m.Step).Name("Step");
            Map(m => m.NextStepMissionID).Name("NextStepMissionID");
            Map(m => m.Title).Name("Title");
            Map(m => m.TrackingID).Name("TrackingID");
            Map(m => m.RewardType).Name("RewardType");
            Map(m => m.RewardID).Name("RewardID");
            Map(m => m.RewardCount).Name("RewardCount");
        }
    }

}
