using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class ChatLog
    {
        [Key("logID")]
        public Int64 logID { get; set; } = 0;
        [Key("accountId")]
        public Int64 accountId { get; set; } = 0;
        [Key("eventId")]
        public string eventId { get; set; } = "";
        [Key("stoveMemberNo")]
        public string stoveMemberNo { get; set; } = "";
        [Key("stoveNicknameNo")]
        public string stoveNicknameNo { get; set; } = "";
        [Key("accountName")]
        public string accountName { get; set; } = "";
        [Key("sessionId")]
        public string sessionId { get; set; } = "";
        [Key("ipAddress")]
        public string ipAddress { get; set; } = "";
        [Key("message")]
        public string message { get; set; } = "";
        [Key("timeStamp")]
        public DateTime timeStamp { get; set; } = DateTime.UtcNow;

        public ChatLog Clone()
        {
            var clone = new ChatLog();
            clone.logID = this.logID;
            clone.accountId = this.accountId;
            clone.eventId = this.eventId;
            clone.stoveMemberNo = this.stoveMemberNo;
            clone.stoveNicknameNo = this.stoveNicknameNo;
            clone.accountName = this.accountName;
            clone.sessionId = this.sessionId;
            clone.ipAddress = this.ipAddress;
            clone.message = this.message;
            clone.timeStamp = this.timeStamp;
            return clone;
        }

        public bool CompareKey(Int64 logID, Int64 accountId)
        {
           return this.logID == logID
                && this.accountId == accountId;
        }

        public bool CompareKey(ChatLog rdata)
        {
           return logID == rdata.logID
                && accountId == rdata.accountId;
        }
    }

}
