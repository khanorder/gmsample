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
    public class PetData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("Grade")]
        [Key("Grade")]
        public EItemGradeType Grade { get; set; } = 0;
        [Name("ActiveSkill")]
        [Key("ActiveSkill")]
        public int ActiveSkill { get; set; } = 0;
        [Name("PassiveSkill")]
        [Key("PassiveSkill")]
        public List<int> PassiveSkill { get; set; } = new List<int>();
        [Name("LikeSection")]
        [Key("LikeSection")]
        public List<int> LikeSection { get; set; } = new List<int>();
    }

    public class PetDataMap : ClassMap<PetData>
    {
        public PetDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Grade).Name("Grade");
            Map(m => m.ActiveSkill).Name("ActiveSkill");
            Map(m => m.PassiveSkill).Name("PassiveSkill");
            Map(m => m.LikeSection).Name("LikeSection");
        }
    }

}
