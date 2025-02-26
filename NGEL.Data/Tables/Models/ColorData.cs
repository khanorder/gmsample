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
    public class ColorData
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Color")]
        [Key("Color")]
        public string Color { get; set; } = "";
    }

    public class ColorDataMap : ClassMap<ColorData>
    {
        public ColorDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Color).Name("Color");
        }
    }

}
