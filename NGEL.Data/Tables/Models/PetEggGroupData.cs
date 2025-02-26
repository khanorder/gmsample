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
    public class PetEggGroupData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("PetEggGroup")]
        [Key("PetEggGroup")]
        public Int64 PetEggGroup { get; set; } = 0;
        [Name("PetID")]
        [Key("PetID")]
        public Int64 PetID { get; set; } = 0;
        [Name("RewardProb")]
        [Key("RewardProb")]
        public Int64 RewardProb { get; set; } = 0;
    }

    public class PetEggGroupDataMap : ClassMap<PetEggGroupData>
    {
        public PetEggGroupDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.PetEggGroup).Name("PetEggGroup");
            Map(m => m.PetID).Name("PetID");
            Map(m => m.RewardProb).Name("RewardProb");
        }
    }

}
