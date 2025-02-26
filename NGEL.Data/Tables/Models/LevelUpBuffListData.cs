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
    public class LevelUpBuffListData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("Stack")]
        [Key("Stack")]
        public int Stack { get; set; } = 0;
        [Name("ModeType")]
        [Key("ModeType")]
        public string ModeType { get; set; } = "";
        [Name("HeroType")]
        [Key("HeroType")]
        public EHeroType HeroType { get; set; } = 0;
        [Name("GroupIndex")]
        [Key("GroupIndex")]
        public int GroupIndex { get; set; } = 0;
        [Name("KeyBuff")]
        [Key("KeyBuff")]
        public bool KeyBuff { get; set; } = false;
        [Name("SlotIndex")]
        [Key("SlotIndex")]
        public int SlotIndex { get; set; } = 0;
        [Name("HeroLevel")]
        [Key("HeroLevel")]
        public int HeroLevel { get; set; } = 0;
        [Name("FavorityLevel")]
        [Key("FavorityLevel")]
        public int FavorityLevel { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
    }

    public class LevelUpBuffListDataMap : ClassMap<LevelUpBuffListData>
    {
        public LevelUpBuffListDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Stack).Name("Stack");
            Map(m => m.ModeType).Name("ModeType");
            Map(m => m.HeroType).Name("HeroType");
            Map(m => m.GroupIndex).Name("GroupIndex");
            Map(m => m.KeyBuff).Name("KeyBuff");
            Map(m => m.SlotIndex).Name("SlotIndex");
            Map(m => m.HeroLevel).Name("HeroLevel");
            Map(m => m.FavorityLevel).Name("FavorityLevel");
            Map(m => m.Name).Name("Name");
        }
    }

}
