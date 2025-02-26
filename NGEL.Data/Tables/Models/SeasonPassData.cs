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
    public class SeasonPassData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("SeasonNum")]
        [Key("SeasonNum")]
        public int SeasonNum { get; set; } = 0;
        [Name("ExtraStartLevel")]
        [Key("ExtraStartLevel")]
        public int ExtraStartLevel { get; set; } = 0;
        [Name("FreeMaxLevel")]
        [Key("FreeMaxLevel")]
        public int FreeMaxLevel { get; set; } = 0;
        [Name("PaidMaxLevel")]
        [Key("PaidMaxLevel")]
        public int PaidMaxLevel { get; set; } = 0;
        [Name("IsActive")]
        [Key("IsActive")]
        public bool IsActive { get; set; } = false;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("StartDate")]
        [Key("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Name("EndDate")]
        [Key("EndDate")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }

    public class SeasonPassDataMap : ClassMap<SeasonPassData>
    {
        public SeasonPassDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.SeasonNum).Name("SeasonNum");
            Map(m => m.ExtraStartLevel).Name("ExtraStartLevel");
            Map(m => m.FreeMaxLevel).Name("FreeMaxLevel");
            Map(m => m.PaidMaxLevel).Name("PaidMaxLevel");
            Map(m => m.IsActive).Name("IsActive");
            Map(m => m.Name).Name("Name");
            Map(m => m.StartDate).Name("StartDate");
            Map(m => m.EndDate).Name("EndDate");
        }
    }

}
