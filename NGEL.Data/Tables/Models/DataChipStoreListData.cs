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
    public class DataChipStoreListData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("ProductItem")]
        [Key("ProductItem")]
        public Int64 ProductItem { get; set; } = 0;
        [Name("ProductType")]
        [Key("ProductType")]
        public string ProductType { get; set; } = "";
        [Name("Enable")]
        [Key("Enable")]
        public bool Enable { get; set; } = false;
        [Name("BuyCostDataChip")]
        [Key("BuyCostDataChip")]
        public Int64 BuyCostDataChip { get; set; } = 0;
    }

    public class DataChipStoreListDataMap : ClassMap<DataChipStoreListData>
    {
        public DataChipStoreListDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.ProductItem).Name("ProductItem");
            Map(m => m.ProductType).Name("ProductType");
            Map(m => m.Enable).Name("Enable");
            Map(m => m.BuyCostDataChip).Name("BuyCostDataChip");
        }
    }

}
