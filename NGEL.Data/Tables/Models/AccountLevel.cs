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
    public class AccountLevel
    {
        [Name("ID")]
        [Key("ID")]
        public int ID { get; set; } = 0;
        [Name("Experience")]
        [Key("Experience")]
        public Int64 Experience { get; set; } = 0;
    }

    public class AccountLevelMap : ClassMap<AccountLevel>
    {
        public AccountLevelMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Experience).Name("Experience");
        }
    }

}
