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
    public class GuideMissionStepReward
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("PreviousReward")]
        [Key("PreviousReward")]
        public Int64 PreviousReward { get; set; } = 0;
        [Name("ReqCompleteMissionCount")]
        [Key("ReqCompleteMissionCount")]
        public Int64 ReqCompleteMissionCount { get; set; } = 0;
        [Name("RewardType")]
        [Key("RewardType")]
        public ERewardType RewardType { get; set; } = 0;
        [Name("RewardID")]
        [Key("RewardID")]
        public string RewardID { get; set; } = "";
        [Name("RewardCount")]
        [Key("RewardCount")]
        public Int64 RewardCount { get; set; } = 0;
        [Name("GradeType")]
        [Key("GradeType")]
        public string GradeType { get; set; } = "";
    }

    public class GuideMissionStepRewardMap : ClassMap<GuideMissionStepReward>
    {
        public GuideMissionStepRewardMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.PreviousReward).Name("PreviousReward");
            Map(m => m.ReqCompleteMissionCount).Name("ReqCompleteMissionCount");
            Map(m => m.RewardType).Name("RewardType");
            Map(m => m.RewardID).Name("RewardID");
            Map(m => m.RewardCount).Name("RewardCount");
            Map(m => m.GradeType).Name("GradeType");
        }
    }

}
