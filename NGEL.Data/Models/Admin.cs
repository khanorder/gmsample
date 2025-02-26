using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class BlockIP
    {
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Key("IPAddress")]
        public string IPAddress { get; set; } = "";
        [Key("StartTime")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        [Key("EndTime")]
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        [Key("Reason")]
        public string Reason { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public BlockIP Clone()
        {
            var clone = new BlockIP();
            clone.ID = this.ID;
            clone.IPAddress = this.IPAddress;
            clone.StartTime = this.StartTime;
            clone.EndTime = this.EndTime;
            clone.Reason = this.Reason;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 ID)
        {
           return this.ID == ID;
        }

        public bool CompareKey(BlockIP rdata)
        {
           return ID == rdata.ID;
        }
    }

    [MessagePackObject]
    public class EventMail
    {
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Key("MailType")]
        public EMailType MailType { get; set; } = 0;
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("Message")]
        public string Message { get; set; } = "";
        [Key("RewardList")]
        public List<MailReward> RewardList { get; set; } = new List<MailReward>();
        [Key("ExpireTime")]
        public int ExpireTime { get; set; } = 0;
        [Key("StartTime")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        [Key("EndTime")]
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public EventMail Clone()
        {
            var clone = new EventMail();
            clone.ID = this.ID;
            clone.MailType = this.MailType;
            clone.Title = this.Title;
            clone.Message = this.Message;
            clone.RewardList.AddRange(this.RewardList);
            clone.ExpireTime = this.ExpireTime;
            clone.StartTime = this.StartTime;
            clone.EndTime = this.EndTime;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 ID)
        {
           return this.ID == ID;
        }

        public bool CompareKey(EventMail rdata)
        {
           return ID == rdata.ID;
        }
    }

    [MessagePackObject]
    public class Maintenance
    {
        [Key("MaintenanceID")]
        public Int64 MaintenanceID { get; set; } = 0;
        [Key("Platform")]
        public Defines.MaintenancePlatform Platform { get; set; } = 0;
        [Key("Area")]
        public string Area { get; set; } = "";
        [Key("State")]
        public Defines.MaintenanceState State { get; set; } = 0;
        [Key("StartTime")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        [Key("EndTime")]
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        [Key("NoticeStrID")]
        public string NoticeStrID { get; set; } = "";
        [Key("UpdateStrID")]
        public string UpdateStrID { get; set; } = "";
        [Key("RecomUpdateStrID")]
        public string RecomUpdateStrID { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;
        [Key("isKicked")]
        public bool isKicked { get; set; } = false;

        public Maintenance Clone()
        {
            var clone = new Maintenance();
            clone.MaintenanceID = this.MaintenanceID;
            clone.Platform = this.Platform;
            clone.Area = this.Area;
            clone.State = this.State;
            clone.StartTime = this.StartTime;
            clone.EndTime = this.EndTime;
            clone.NoticeStrID = this.NoticeStrID;
            clone.UpdateStrID = this.UpdateStrID;
            clone.RecomUpdateStrID = this.RecomUpdateStrID;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            clone.isKicked = this.isKicked;
            return clone;
        }

        public bool CompareKey(Int64 MaintenanceID)
        {
           return this.MaintenanceID == MaintenanceID;
        }

        public bool CompareKey(Maintenance rdata)
        {
           return MaintenanceID == rdata.MaintenanceID;
        }
    }

    [MessagePackObject]
    public class NoticeBanner
    {
        [Key("BannerID")]
        public Int64 BannerID { get; set; } = 0;
        [Key("StartAt")]
        public int StartAt { get; set; } = 0;
        [Key("EndAt")]
        public int EndAt { get; set; } = 0;
        [Key("ImageURL")]
        public string ImageURL { get; set; } = "";
        [Key("Title")]
        public string Title { get; set; } = "";
        [Key("Message")]
        public string Message { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public NoticeBanner Clone()
        {
            var clone = new NoticeBanner();
            clone.BannerID = this.BannerID;
            clone.StartAt = this.StartAt;
            clone.EndAt = this.EndAt;
            clone.ImageURL = this.ImageURL;
            clone.Title = this.Title;
            clone.Message = this.Message;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 BannerID)
        {
           return this.BannerID == BannerID;
        }

        public bool CompareKey(NoticeBanner rdata)
        {
           return BannerID == rdata.BannerID;
        }
    }

    [MessagePackObject]
    public class Slang
    {
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Key("Word")]
        public string Word { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Slang Clone()
        {
            var clone = new Slang();
            clone.ID = this.ID;
            clone.Word = this.Word;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 ID)
        {
           return this.ID == ID;
        }

        public bool CompareKey(Slang rdata)
        {
           return ID == rdata.ID;
        }
    }

    [MessagePackObject]
    public class WhiteList
    {
        [Key("ID")]
        public Int64 ID { get; set; } = 0;
        [Key("DeviceID")]
        public string DeviceID { get; set; } = "";
        [Key("MemberNo")]
        public UInt64 MemberNo { get; set; } = 0;
        [Key("Comment")]
        public string Comment { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("UpdateAt")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public WhiteList Clone()
        {
            var clone = new WhiteList();
            clone.ID = this.ID;
            clone.DeviceID = this.DeviceID;
            clone.MemberNo = this.MemberNo;
            clone.Comment = this.Comment;
            clone.CreateAt = this.CreateAt;
            clone.UpdateAt = this.UpdateAt;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Int64 ID)
        {
           return this.ID == ID;
        }

        public bool CompareKey(WhiteList rdata)
        {
           return ID == rdata.ID;
        }
    }

}
