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
    public class SkillData
    {
        [Name("SkillID")]
        [Key("SkillID")]
        public int SkillID { get; set; } = 0;
        [Name("DesignName")]
        [Key("DesignName")]
        public string DesignName { get; set; } = "";
        [Name("SkillType")]
        [Key("SkillType")]
        public string SkillType { get; set; } = "";
        [Name("IsPetSkill")]
        [Key("IsPetSkill")]
        public bool IsPetSkill { get; set; } = false;
    }

    public class SkillDataMap : ClassMap<SkillData>
    {
        public SkillDataMap()
        {
            Map(m => m.SkillID).Name("SkillID");
            Map(m => m.DesignName).Name("DesignName");
            Map(m => m.SkillType).Name("SkillType");
            Map(m => m.IsPetSkill).Name("IsPetSkill");
        }
    }

}
