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
    public class ParameterData
    {
        [Name("ParameterName")]
        [Key("ParameterName")]
        public string ParameterName { get; set; } = "";
        [Name("Name")]
        [Key("Name")]
        public string Name { get; set; } = "";
        [Key("NameString")]
        public string NameString { get; set; } = "";
        [Key("NameStringWithParameter")]
        public string NameStringWithParameter { get; set; } = "";
        [Name("SortIndex")]
        [Key("SortIndex")]
        public int SortIndex { get; set; } = 0;
        [Name("CalcType")]
        [Key("CalcType")]
        public string CalcType { get; set; } = "";
        [Name("CalcValue")]
        [Key("CalcValue")]
        public int CalcValue { get; set; } = 0;
        [Name("DigitCount")]
        [Key("DigitCount")]
        public int DigitCount { get; set; } = 0;
        [Name("AddString")]
        [Key("AddString")]
        public int AddString { get; set; } = 0;
    }

    public class ParameterDataMap : ClassMap<ParameterData>
    {
        public ParameterDataMap()
        {
            Map(m => m.ParameterName).Name("ParameterName");
            Map(m => m.Name).Name("Name");
            Map(m => m.SortIndex).Name("SortIndex");
            Map(m => m.CalcType).Name("CalcType");
            Map(m => m.CalcValue).Name("CalcValue");
            Map(m => m.DigitCount).Name("DigitCount");
            Map(m => m.AddString).Name("AddString");
        }
    }

}
