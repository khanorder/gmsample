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
    public class ExpressionData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("Character")]
        [Key("Character")]
        public EHeroType Character { get; set; } = 0;
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("ExpressionType")]
        [Key("ExpressionType")]
        public EExpressionType ExpressionType { get; set; } = 0;
        [Name("ExpressionCategory")]
        [Key("ExpressionCategory")]
        public EExpressionCategory ExpressionCategory { get; set; } = 0;
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithID")]
        public string NameStringWithID { get; set; } = "";
    }

    public class ExpressionDataMap : ClassMap<ExpressionData>
    {
        public ExpressionDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.Character).Name("Character");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.ExpressionType).Name("ExpressionType");
            Map(m => m.ExpressionCategory).Name("ExpressionCategory");
            Map(m => m.Name).Name("Name");
        }
    }

}
