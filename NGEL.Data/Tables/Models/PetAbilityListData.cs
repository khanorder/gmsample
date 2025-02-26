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
    public class PetAbilityListData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("AbilityType")]
        [Key("AbilityType")]
        public string AbilityType { get; set; } = "";
        [Name("AbilityGroup")]
        [Key("AbilityGroup")]
        public int AbilityGroup { get; set; } = 0;
        [Name("AbilityValue")]
        [Key("AbilityValue")]
        public List<double> AbilityValue { get; set; } = new List<double>();
        [Name("AbilityRate")]
        [Key("AbilityRate")]
        public List<double> AbilityRate { get; set; } = new List<double>();
    }

    public class PetAbilityListDataMap : ClassMap<PetAbilityListData>
    {
        public PetAbilityListDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.AbilityType).Name("AbilityType");
            Map(m => m.AbilityGroup).Name("AbilityGroup");
            Map(m => m.AbilityValue).Name("AbilityValue");
            Map(m => m.AbilityRate).Name("AbilityRate");
        }
    }

}
