using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class VisitLog
    {
        [Key("id")]
        public Int64 id { get; set; } = 0;
        [Key("data")]
        public string data { get; set; } = "";

        public VisitLog Clone()
        {
            var clone = new VisitLog();
            clone.id = this.id;
            clone.data = this.data;
            return clone;
        }

        public bool CompareKey(Int64 id)
        {
           return this.id == id;
        }

        public bool CompareKey(VisitLog rdata)
        {
           return id == rdata.id;
        }
    }

}
