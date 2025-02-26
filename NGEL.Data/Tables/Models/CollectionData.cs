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
    public class CollectionData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Type")]
        [Key("Type")]
        public ECollectionType Type { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("Group")]
        [Key("Group")]
        public int Group { get; set; } = 0;
        [Name("GroupName")]
        [Key("GroupName")]
        public string GroupName { get; set; } = "";
        [Key("GroupNameString")]
        public string GroupNameString { get; set; } = "";
        [Key("GroupNameStringWithID")]
        public string GroupNameStringWithID { get; set; } = "";
        [Name("ReqID")]
        [Key("ReqID")]
        public Int64 ReqID { get; set; } = 0;
    }

    public class CollectionDataMap : ClassMap<CollectionData>
    {
        public CollectionDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Type).Name("Type");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.Group).Name("Group");
            Map(m => m.GroupName).Name("GroupName");
            Map(m => m.ReqID).Name("ReqID");
        }
    }

}
