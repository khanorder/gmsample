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
    public class BlockContentData
    {
        [Name("SeqID")]
        [Key("SeqID")]
        public Int64 SeqID { get; set; } = 0;
        [Name("PacketID")]
        [Key("PacketID")]
        public string PacketID { get; set; } = "";
        [Name("PacketName")]
        [Key("PacketName")]
        public string PacketName { get; set; } = "";
    }

    public class BlockContentDataMap : ClassMap<BlockContentData>
    {
        public BlockContentDataMap()
        {
            Map(m => m.SeqID).Name("SeqID");
            Map(m => m.PacketID).Name("PacketID");
            Map(m => m.PacketName).Name("PacketName");
        }
    }

}
