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
    public class NavMenuData
    {
        [Name("Path")]
        [Key("Path")]
        public string Path { get; set; } = "";
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
    }

    public class NavMenuDataMap : ClassMap<NavMenuData>
    {
        public NavMenuDataMap()
        {
            Map(m => m.Path).Name("Path");
            Map(m => m.Name).Name("Name");
        }
    }

}
