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
    public class WorldMapData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("WorldmapPopTitle")]
        [Key("WorldmapPopTitle")]
        public string WorldmapPopTitle { get; set; } = "";
        [Key("WorldmapPopTitleString")]
        public string WorldmapPopTitleString { get; set; } = "";
        [Key("WorldmapPopTitleStringWithID")]
        public string WorldmapPopTitleStringWithID { get; set; } = "";
    }

    public class WorldMapDataMap : ClassMap<WorldMapData>
    {
        public WorldMapDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.WorldmapPopTitle).Name("WorldmapPopTitle");
        }
    }

}
