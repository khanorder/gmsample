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
    public class QuestData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
        [Name("QuestType")]
        [Key("QuestType")]
        public string QuestType { get; set; } = "";
        [Name("MissionCount")]
        [Key("MissionCount")]
        public int MissionCount { get; set; } = 0;
        [Name("MissionIDList")]
        [Key("MissionIDList")]
        public string MissionIDList { get; set; } = "";
        [Name("CompleteRewardItemID")]
        [Key("CompleteRewardItemID")]
        public Int64 CompleteRewardItemID { get; set; } = 0;
        [Name("CompleteRewardItemCount")]
        [Key("CompleteRewardItemCount")]
        public Int64 CompleteRewardItemCount { get; set; } = 0;
    }

    public class QuestDataMap : ClassMap<QuestData>
    {
        public QuestDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Name).Name("Name");
            Map(m => m.QuestType).Name("QuestType");
            Map(m => m.MissionCount).Name("MissionCount");
            Map(m => m.MissionIDList).Name("MissionIDList");
            Map(m => m.CompleteRewardItemID).Name("CompleteRewardItemID");
            Map(m => m.CompleteRewardItemCount).Name("CompleteRewardItemCount");
        }
    }

}
