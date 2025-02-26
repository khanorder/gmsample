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
    public class AttendanceData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Description")]
        [Key("Description")]
        public string Description { get; set; } = "";
        [Key("DescriptionString")]
        public string DescriptionString { get; set; } = "";
        [Key("DescriptionStringWithID")]
        public string DescriptionStringWithID { get; set; } = "";
        [Name("RewardTotalCount")]
        [Key("RewardTotalCount")]
        public int RewardTotalCount { get; set; } = 0;
        [Name("RewardGroup")]
        [Key("RewardGroup")]
        public int RewardGroup { get; set; } = 0;
    }

    public class AttendanceDataMap : ClassMap<AttendanceData>
    {
        public AttendanceDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Description).Name("Description");
            Map(m => m.RewardTotalCount).Name("RewardTotalCount");
            Map(m => m.RewardGroup).Name("RewardGroup");
        }
    }

}
