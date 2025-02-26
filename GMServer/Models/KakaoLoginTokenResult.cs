namespace GMServer.Models
{
    public class KakaoLoginTokenResult
    {
        public string access_token { get; set; } = "";
        public string token_type { get; set; } = "";
        public string refresh_token { get; set; } = "";
        public string id_token { get; set; } = "";
        public int expires_in { get; set; } = 0;
        public string scope { get; set; } = "";
        public int refresh_token_expires_in { get; set; } = 0;
    }

    public class KakaoLoginUserResult
    {
        public Int64 id { get; set; } = 0;
        public string connected_at { get; set; } = "";
        public KakaoLoginProperties? properties { get; set; } = null;
        public KakaoLoginAccount? kakao_account { get; set; } = null;
    }

    public class KakaoLoginProperties
    {
        public string nickname { get; set; } = "";
        public string profile_image { get; set; } = "";
        public string thumbnail_image { get; set; } = "";
    }

    public class KakaoLoginAccount
    {
        public bool profile_nickname_needs_agreement { get; set; } = false;
        public bool profile_image_needs_agreement { get; set; } = false;
        public string thumbnail_image { get; set; } = "";
        public KakaoLoginAccountProfile? profile { get; set; } = null;
        public bool has_email { get; set; } = false;
        public bool email_needs_agreement { get; set; } = false;
        public bool is_email_valid { get; set; } = false;
        public bool is_email_verified { get; set; } = false;
        public string email { get; set; } = "";
    }

    public class KakaoLoginAccountProfile
    {
        public string nickname { get; set; } = "";
        public string thumbnail_image_url { get; set; } = "";
        public string profile_image_url { get; set; } = "";
        public bool is_default_image { get; set; } = false;
    }

    public class KakaoLoginUser
    {
        public Int64 id { get; set; } = 0;
        public string nickname { get; set; } = "";
        public string age { get; set; } = "";
        public string gender { get; set; } = "";
        public string email { get; set; } = "";
        public string name { get; set; } = "";
    }
}
