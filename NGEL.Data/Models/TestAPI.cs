using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Models
{

    [MessagePackObject]
    public class TestSignInParameters
    {
        [Key("name")]
        public string name { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class TestSignInResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class TestSignOutParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class TestSignOutResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class TestSignUpParameters
    {
        [Key("name")]
        public string name { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class TestSignUpResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class TestTesterSignInParameters
    {
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class TestTesterSignInResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

}
