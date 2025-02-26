namespace GMServer.Models
{
    public class NaverLoginTokenResult
    {
        public string access_token { get; set; } = "";
        public string refresh_token { get; set; } = "";
        public string token_type { get; set; } = "";
        public string expires_in { get; set; } = "";
    }

    public class NaverLoginUserResult
    {
        public string resultcode { get; set; } = "";
        public string message { get; set; } = "";
        public NaverLoginUser? response { get; set; } = null;
    }

    public class NaverLoginUser
    {
        public string id { get; set; } = "";
        public string nickname { get; set; } = "";
        public string profile_image { get; set; } = "";
        public string age { get; set; } = "";
        public string gender { get; set; } = "";
        public string email { get; set; } = "";
        public string mobile { get; set; } = "";
        public string mobile_e164 { get; set; } = "";
        public string name { get; set; } = "";
        public string birthday { get; set; } = "";
        public string birthyear { get; set; } = "";
    }
}
