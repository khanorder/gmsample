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
    public class PetEggData
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
        [Name("PetEggGroupID")]
        [Key("PetEggGroupID")]
        public Int64 PetEggGroupID { get; set; } = 0;
        [Name("ConsumeAssetType")]
        [Key("ConsumeAssetType")]
        public List<EUserAssetType> ConsumeAssetType { get; set; } = new List<EUserAssetType>();
        [Name("ConsumeAssetCount")]
        [Key("ConsumeAssetCount")]
        public List<Int64> ConsumeAssetCount { get; set; } = new List<Int64>();
        [Name("ReduceIncubateTime")]
        [Key("ReduceIncubateTime")]
        public Int64 ReduceIncubateTime { get; set; } = 0;
    }

    public class PetEggDataMap : ClassMap<PetEggData>
    {
        public PetEggDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Name).Name("Name");
            Map(m => m.PetEggGroupID).Name("PetEggGroupID");
            Map(m => m.ConsumeAssetType).Name("ConsumeAssetType");
            Map(m => m.ConsumeAssetCount).Name("ConsumeAssetCount");
            Map(m => m.ReduceIncubateTime).Name("ReduceIncubateTime");
        }
    }

}
