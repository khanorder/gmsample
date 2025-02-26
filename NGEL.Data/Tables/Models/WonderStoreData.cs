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
    public class WonderStoreData
    {
        [Name("ID")]
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Name("StoreGoodsType")]
        [Key("StoreGoodsType")]
        public EStoreGoodsType StoreGoodsType { get; set; } = 0;
        [Name("MainType")]
        [Key("MainType")]
        public string MainType { get; set; } = "";
        [Name("SubType")]
        [Key("SubType")]
        public string SubType { get; set; } = "";
        [Name("IAPUse")]
        [Key("IAPUse")]
        public bool IAPUse { get; set; } = false;
        [Name("ProductID_QA_PC")]
        [Key("ProductID_QA_PC")]
        public string ProductID_QA_PC { get; set; } = "";
        [Name("ProductID_QA_AOS")]
        [Key("ProductID_QA_AOS")]
        public string ProductID_QA_AOS { get; set; } = "";
        [Name("ProductID_QA_IOS")]
        [Key("ProductID_QA_IOS")]
        public string ProductID_QA_IOS { get; set; } = "";
        [Name("ProductID_LIVE_PC")]
        [Key("ProductID_LIVE_PC")]
        public string ProductID_LIVE_PC { get; set; } = "";
        [Name("ProductID_LIVE_AOS")]
        [Key("ProductID_LIVE_AOS")]
        public string ProductID_LIVE_AOS { get; set; } = "";
        [Name("ProductID_LIVE_IOS")]
        [Key("ProductID_LIVE_IOS")]
        public string ProductID_LIVE_IOS { get; set; } = "";
        [Name("RewardTableID")]
        [Key("RewardTableID")]
        public Int64 RewardTableID { get; set; } = 0;
        [Name("SaleText")]
        [Key("SaleText")]
        public string SaleText { get; set; } = "";
        [Name("ReqAssetID")]
        [Key("ReqAssetID")]
        public EUserAssetType ReqAssetID { get; set; } = 0;
        [Name("ReqAssetCount")]
        [Key("ReqAssetCount")]
        public Int64 ReqAssetCount { get; set; } = 0;
        [Name("DateSwitch")]
        [Key("DateSwitch")]
        public bool DateSwitch { get; set; } = false;
        [Name("StartDate")]
        [Key("StartDate")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Name("EndDate")]
        [Key("EndDate")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        [Name("LimitTime")]
        [Key("LimitTime")]
        public ELimitTime LimitTime { get; set; } = 0;
        [Name("LimitCount")]
        [Key("LimitCount")]
        public Int64 LimitCount { get; set; } = 0;
        [Name("ProductName")]
        [Key("ProductName")]
        public string ProductName { get; set; } = "";
        [Key("ProductNameString")]
        public string ProductNameString { get; set; } = "";
        [Key("ProductNameStringWithID")]
        public string ProductNameStringWithID { get; set; } = "";
    }

    public class WonderStoreDataMap : ClassMap<WonderStoreData>
    {
        public WonderStoreDataMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.StoreGoodsType).Name("StoreGoodsType");
            Map(m => m.MainType).Name("MainType");
            Map(m => m.SubType).Name("SubType");
            Map(m => m.IAPUse).Name("IAPUse");
            Map(m => m.ProductID_QA_PC).Name("ProductID_QA_PC");
            Map(m => m.ProductID_QA_AOS).Name("ProductID_QA_AOS");
            Map(m => m.ProductID_QA_IOS).Name("ProductID_QA_IOS");
            Map(m => m.ProductID_LIVE_PC).Name("ProductID_LIVE_PC");
            Map(m => m.ProductID_LIVE_AOS).Name("ProductID_LIVE_AOS");
            Map(m => m.ProductID_LIVE_IOS).Name("ProductID_LIVE_IOS");
            Map(m => m.RewardTableID).Name("RewardTableID");
            Map(m => m.SaleText).Name("SaleText");
            Map(m => m.ReqAssetID).Name("ReqAssetID");
            Map(m => m.ReqAssetCount).Name("ReqAssetCount");
            Map(m => m.DateSwitch).Name("DateSwitch");
            Map(m => m.StartDate).Name("StartDate");
            Map(m => m.EndDate).Name("EndDate");
            Map(m => m.LimitTime).Name("LimitTime");
            Map(m => m.LimitCount).Name("LimitCount");
            Map(m => m.ProductName).Name("ProductName");
        }
    }

}
