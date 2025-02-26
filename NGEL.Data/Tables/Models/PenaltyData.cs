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
    public class PenaltyData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("ReportState")]
        [Key("ReportState")]
        public EPenaltyReportState ReportState { get; set; } = 0;
        [Name("PenaltyGrade")]
        [Key("PenaltyGrade")]
        public int PenaltyGrade { get; set; } = 0;
        [Name("ReqPenaltyPoint")]
        [Key("ReqPenaltyPoint")]
        public int ReqPenaltyPoint { get; set; } = 0;
        [Name("PenaltyType")]
        [Key("PenaltyType")]
        public EPenaltyType PenaltyType { get; set; } = 0;
        [Name("PenaltyTime")]
        [Key("PenaltyTime")]
        public int PenaltyTime { get; set; } = 0;
        [Name("PenaltyCount")]
        [Key("PenaltyCount")]
        public int PenaltyCount { get; set; } = 0;
        [Name("ClearPenaltyTime")]
        [Key("ClearPenaltyTime")]
        public int ClearPenaltyTime { get; set; } = 0;
        [Name("ClearPenaltyGrade")]
        [Key("ClearPenaltyGrade")]
        public int ClearPenaltyGrade { get; set; } = 0;
        [Key("ReportStateText")]
        public string ReportStateText { get; set; } = "";
        [Key("ReportStateTextString")]
        public string ReportStateTextString { get; set; } = "";
        [Key("ReportStateTextStringWithID")]
        public string ReportStateTextStringWithID { get; set; } = "";
        [Key("PenaltyTypeText")]
        public string PenaltyTypeText { get; set; } = "";
        [Key("PenaltyTypeTextString")]
        public string PenaltyTypeTextString { get; set; } = "";
        [Key("PenaltyTypeTextStringWithID")]
        public string PenaltyTypeTextStringWithID { get; set; } = "";
    }

    public class PenaltyDataMap : ClassMap<PenaltyData>
    {
        public PenaltyDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.ReportState).Name("ReportState");
            Map(m => m.PenaltyGrade).Name("PenaltyGrade");
            Map(m => m.ReqPenaltyPoint).Name("ReqPenaltyPoint");
            Map(m => m.PenaltyType).Name("PenaltyType");
            Map(m => m.PenaltyTime).Name("PenaltyTime");
            Map(m => m.PenaltyCount).Name("PenaltyCount");
            Map(m => m.ClearPenaltyTime).Name("ClearPenaltyTime");
            Map(m => m.ClearPenaltyGrade).Name("ClearPenaltyGrade");
        }
    }

}
