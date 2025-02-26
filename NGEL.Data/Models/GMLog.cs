using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class GMLog
    {
        [Key("id")]
        public UInt64 id { get; set; } = 0;
        [Key("userId")]
        public string userId { get; set; } = "";
        [Key("type")]
        public Defines.GMLogType type { get; set; } = 0;
        [Key("methodId")]
        public Int64 methodId { get; set; } = 0;
        [Key("urlId")]
        public Int64 urlId { get; set; } = 0;
        [Key("errorId")]
        public Int64 errorId { get; set; } = 0;
        [Key("userAgentId")]
        public Int64 userAgentId { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
        [Key("remoteAddress")]
        public string remoteAddress { get; set; } = "";
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;

        public GMLog Clone()
        {
            var clone = new GMLog();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.type = this.type;
            clone.methodId = this.methodId;
            clone.urlId = this.urlId;
            clone.errorId = this.errorId;
            clone.userAgentId = this.userAgentId;
            clone.message = this.message;
            clone.remoteAddress = this.remoteAddress;
            clone.regTime = this.regTime;
            return clone;
        }

        public bool CompareKey(UInt64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMLog rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class GMCombinedLog
    {
        [Key("id")]
        public UInt64 id { get; set; } = 0;
        [Key("userId")]
        public string userId { get; set; } = "";
        [Key("userName")]
        public string userName { get; set; } = "";
        [Key("type")]
        public Defines.GMLogType type { get; set; } = 0;
        [Key("methodId")]
        public Int64 methodId { get; set; } = 0;
        [Key("methodName")]
        public string methodName { get; set; } = "";
        [Key("urlId")]
        public Int64 urlId { get; set; } = 0;
        [Key("urlName")]
        public string urlName { get; set; } = "";
        [Key("errorId")]
        public Int64 errorId { get; set; } = 0;
        [Key("errorName")]
        public string errorName { get; set; } = "";
        [Key("userAgentId")]
        public Int64 userAgentId { get; set; } = 0;
        [Key("userAgentName")]
        public string userAgentName { get; set; } = "";
        [Key("message")]
        public string message { get; set; } = "";
        [Key("remoteAddress")]
        public string remoteAddress { get; set; } = "";
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;

        public GMCombinedLog Clone()
        {
            var clone = new GMCombinedLog();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.userName = this.userName;
            clone.type = this.type;
            clone.methodId = this.methodId;
            clone.methodName = this.methodName;
            clone.urlId = this.urlId;
            clone.urlName = this.urlName;
            clone.errorId = this.errorId;
            clone.errorName = this.errorName;
            clone.userAgentId = this.userAgentId;
            clone.userAgentName = this.userAgentName;
            clone.message = this.message;
            clone.remoteAddress = this.remoteAddress;
            clone.regTime = this.regTime;
            return clone;
        }

        public bool CompareKey(UInt64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMCombinedLog rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class GMLogError
    {
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("name")]
        public string name { get; set; } = "";

        public GMLogError Clone()
        {
            var clone = new GMLogError();
            clone.id = this.id;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Int64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMLogError rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class GMLogMethod
    {
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("name")]
        public string name { get; set; } = "";

        public GMLogMethod Clone()
        {
            var clone = new GMLogMethod();
            clone.id = this.id;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Int64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMLogMethod rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class GMLogURL
    {
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("name")]
        public string name { get; set; } = "";

        public GMLogURL Clone()
        {
            var clone = new GMLogURL();
            clone.id = this.id;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Int64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMLogURL rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class GMLogUserAgent
    {
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("name")]
        public string name { get; set; } = "";

        public GMLogUserAgent Clone()
        {
            var clone = new GMLogUserAgent();
            clone.id = this.id;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Int64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(GMLogUserAgent rdata)
        {
           return id == rdata.id;
        }
    }

}
