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
    public class ArtifactData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("Default")]
        [Key("Default")]
        public bool Default { get; set; } = false;
        [Name("ArtifactSet")]
        [Key("ArtifactSet")]
        public string ArtifactSet { get; set; } = "";
        [Name("Group")]
        [Key("Group")]
        public int Group { get; set; } = 0;
        [Name("Grade")]
        [Key("Grade")]
        public int Grade { get; set; } = 0;
        [Name("MaxEnhance")]
        [Key("MaxEnhance")]
        public int MaxEnhance { get; set; } = 0;
    }

    public class ArtifactDataMap : ClassMap<ArtifactData>
    {
        public ArtifactDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Name).Name("Name");
            Map(m => m.Default).Name("Default");
            Map(m => m.ArtifactSet).Name("ArtifactSet");
            Map(m => m.Group).Name("Group");
            Map(m => m.Grade).Name("Grade");
            Map(m => m.MaxEnhance).Name("MaxEnhance");
        }
    }

}
