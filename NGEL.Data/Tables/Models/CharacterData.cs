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
    public class CharacterData
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
        [Name("HeroType")]
        [Key("HeroType")]
        public EHeroType HeroType { get; set; } = 0;
        [Name("NPCType")]
        [Key("NPCType")]
        public ENpcType NPCType { get; set; } = ENpcType.Normal;
    }

    public class CharacterDataMap : ClassMap<CharacterData>
    {
        public CharacterDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Name).Name("Name");
            Map(m => m.HeroType).Name("HeroType");
            Map(m => m.NPCType).Name("NPCType");
        }
    }

}
