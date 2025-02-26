using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class BiskitLog
    {
        [Key("logID")]
        public Int64 logID { get; set; } = 0;
        [Key("eventGroupID")]
        public string eventGroupID { get; set; } = "";
        [Key("eventID")]
        public string eventID { get; set; } = "";
        [Key("timestamp")]
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
        [Key("sequenceNumber")]
        public Int64 sequenceNumber { get; set; } = 0;
        [Key("stoveMemberNO")]
        public Int64 stoveMemberNO { get; set; } = 0;
        [Key("stoveNickNameNO")]
        public Int64 stoveNickNameNO { get; set; } = 0;
        [Key("accountID")]
        public Int64 accountID { get; set; } = 0;
        [Key("accountLevel")]
        public int accountLevel { get; set; } = 0;
        [Key("accountName")]
        public string accountName { get; set; } = "";
        [Key("characterID")]
        public Int64 characterID { get; set; } = 0;
        [Key("characterLevel")]
        public int characterLevel { get; set; } = 0;
        [Key("sessionID")]
        public string sessionID { get; set; } = "";
        [Key("marketCode")]
        public string marketCode { get; set; } = "";
        [Key("serverCode")]
        public string serverCode { get; set; } = "";
        [Key("channelCode")]
        public string channelCode { get; set; } = "";
        [Key("ipAddress")]
        public string ipAddress { get; set; } = "";
        [Key("deviceID")]
        public string deviceID { get; set; } = "";
        [Key("deviceType")]
        public string deviceType { get; set; } = "";
        [Key("deviceModel")]
        public string deviceModel { get; set; } = "";
        [Key("os")]
        public string os { get; set; } = "";
        [Key("v01")]
        public string v01 { get; set; } = "";
        [Key("v02")]
        public string v02 { get; set; } = "";
        [Key("v03")]
        public string v03 { get; set; } = "";
        [Key("v04")]
        public string v04 { get; set; } = "";
        [Key("v05")]
        public string v05 { get; set; } = "";
        [Key("v06")]
        public string v06 { get; set; } = "";
        [Key("v07")]
        public string v07 { get; set; } = "";
        [Key("v08")]
        public string v08 { get; set; } = "";
        [Key("v09")]
        public string v09 { get; set; } = "";
        [Key("v10")]
        public string v10 { get; set; } = "";
        [Key("v11")]
        public string v11 { get; set; } = "";
        [Key("v12")]
        public string v12 { get; set; } = "";
        [Key("v13")]
        public string v13 { get; set; } = "";
        [Key("v14")]
        public string v14 { get; set; } = "";
        [Key("v15")]
        public string v15 { get; set; } = "";
        [Key("v16")]
        public string v16 { get; set; } = "";
        [Key("v17")]
        public string v17 { get; set; } = "";
        [Key("v18")]
        public string v18 { get; set; } = "";
        [Key("v19")]
        public string v19 { get; set; } = "";
        [Key("v20")]
        public string v20 { get; set; } = "";
        [Key("v21")]
        public string v21 { get; set; } = "";
        [Key("v22")]
        public string v22 { get; set; } = "";
        [Key("v23")]
        public string v23 { get; set; } = "";
        [Key("v24")]
        public string v24 { get; set; } = "";
        [Key("v25")]
        public string v25 { get; set; } = "";
        [Key("v26")]
        public string v26 { get; set; } = "";
        [Key("v27")]
        public string v27 { get; set; } = "";
        [Key("v28")]
        public string v28 { get; set; } = "";
        [Key("v29")]
        public string v29 { get; set; } = "";
        [Key("v30")]
        public string v30 { get; set; } = "";

        public BiskitLog Clone()
        {
            var clone = new BiskitLog();
            clone.logID = this.logID;
            clone.eventGroupID = this.eventGroupID;
            clone.eventID = this.eventID;
            clone.timestamp = this.timestamp;
            clone.sequenceNumber = this.sequenceNumber;
            clone.stoveMemberNO = this.stoveMemberNO;
            clone.stoveNickNameNO = this.stoveNickNameNO;
            clone.accountID = this.accountID;
            clone.accountLevel = this.accountLevel;
            clone.accountName = this.accountName;
            clone.characterID = this.characterID;
            clone.characterLevel = this.characterLevel;
            clone.sessionID = this.sessionID;
            clone.marketCode = this.marketCode;
            clone.serverCode = this.serverCode;
            clone.channelCode = this.channelCode;
            clone.ipAddress = this.ipAddress;
            clone.deviceID = this.deviceID;
            clone.deviceType = this.deviceType;
            clone.deviceModel = this.deviceModel;
            clone.os = this.os;
            clone.v01 = this.v01;
            clone.v02 = this.v02;
            clone.v03 = this.v03;
            clone.v04 = this.v04;
            clone.v05 = this.v05;
            clone.v06 = this.v06;
            clone.v07 = this.v07;
            clone.v08 = this.v08;
            clone.v09 = this.v09;
            clone.v10 = this.v10;
            clone.v11 = this.v11;
            clone.v12 = this.v12;
            clone.v13 = this.v13;
            clone.v14 = this.v14;
            clone.v15 = this.v15;
            clone.v16 = this.v16;
            clone.v17 = this.v17;
            clone.v18 = this.v18;
            clone.v19 = this.v19;
            clone.v20 = this.v20;
            clone.v21 = this.v21;
            clone.v22 = this.v22;
            clone.v23 = this.v23;
            clone.v24 = this.v24;
            clone.v25 = this.v25;
            clone.v26 = this.v26;
            clone.v27 = this.v27;
            clone.v28 = this.v28;
            clone.v29 = this.v29;
            clone.v30 = this.v30;
            return clone;
        }

        public bool CompareKey(Int64 logID)
        {
           return this.logID == logID;
        }

        public bool CompareKey(BiskitLog rdata)
        {
           return logID == rdata.logID;
        }
    }

}
