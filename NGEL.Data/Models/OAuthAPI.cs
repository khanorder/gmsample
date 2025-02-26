using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NGEL.Data.Tables.Models;

namespace NGEL.Data.Models
{

    [MessagePackObject]
    public class OAuthCheckConfirmEmailParameters
    {
        [Key("id")]
        public string id { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthCheckConfirmEmailResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class OAuthInitEmailPasswordParameters
    {
        [Key("id")]
        public string id { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthInitEmailPasswordResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class OAuthSignInParameters
    {
        [Key("provider")]
        public Defines.OAuthProvider provider { get; set; } = 0;
        [Key("code")]
        public string code { get; set; } = "";
        [Key("stateCode")]
        public string stateCode { get; set; } = "";
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignInResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("token")]
        public string token { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignInLDAPParameters
    {
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("email")]
        public string email { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignInLDAPResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("token")]
        public string token { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignInEmailParameters
    {
        [Key("clientId")]
        public string clientId { get; set; } = "";
        [Key("email")]
        public string email { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignInEmailResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
        [Key("token")]
        public string token { get; set; } = "";
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
    }

    [MessagePackObject]
    public class OAuthCheckAuthenticationParameters
    {
        [Key("token")]
        public string token { get; set; } = "";
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthCheckAuthenticationResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

    [MessagePackObject]
    public class OAuthSignOutParameters
    {
        [Key("requestURL")]
        public string requestURL { get; set; } = "";
    }

    [MessagePackObject]
    public class OAuthSignOutResponses : IAPIResponse
    {
        [Key("result")]
        public bool result { get; set; } = false;
        [Key("error")]
        public Errors error { get; set; } = Errors.None;
        [Key("user")]
        public SignInUser? user { get; set; } = null;
    }

}
