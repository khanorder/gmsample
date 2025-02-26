using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Models
{

    [MessagePackObject]
    public class ATestStompTestParameters
    {
        [Key("NoticeMessage")]
        public string NoticeMessage { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class ATestStompTestResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

}
