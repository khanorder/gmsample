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
    public class MissionData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("MissionGroup")]
        [Key("MissionGroup")]
        public EMissionGroup MissionGroup { get; set; } = 0;
        [Name("EXPGroup")]
        [Key("EXPGroup")]
        public int EXPGroup { get; set; } = 0;
        [Name("CreateCount")]
        [Key("CreateCount")]
        public int CreateCount { get; set; } = 0;
        [Name("UseDate")]
        [Key("UseDate")]
        public bool UseDate { get; set; } = false;
        [Name("StartDate")]
        [Key("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Name("EndDate")]
        [Key("EndDate")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }

    public class MissionDataMap : ClassMap<MissionData>
    {
        public MissionDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.MissionGroup).Name("MissionGroup");
            Map(m => m.EXPGroup).Name("EXPGroup");
            Map(m => m.CreateCount).Name("CreateCount");
            Map(m => m.UseDate).Name("UseDate");
            Map(m => m.StartDate).Name("StartDate");
            Map(m => m.EndDate).Name("EndDate");
        }
    }

}
