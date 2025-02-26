namespace NGEL.Data.Settings
{
    public class JwtSettings
    {
        public bool validateIssuerSigningKey { get; set; }
        public string issuerSigningKey { get; set; } = "";
        public bool validateIssuer { get; set; } = true;
        public string validIssuer { get; set; } = "";
        public bool validateAudience { get; set; } = true;
        public string[] validAudiences { get; set; } = {};
        public bool requireExpirationTime { get; set; }
        public bool validateLifetime { get; set; } = true;
        public int expireMinutes { get; set; } = 5;
    }
}
