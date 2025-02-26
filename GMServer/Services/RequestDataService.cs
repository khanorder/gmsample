namespace GMServer.Services
{
    public class RequestDataService
    {
        public string serverAddress { get; set; } = "";
        public string remoteAddress { get; set; } = "";
        public string scheme { get; set; } = "http";
        public int port { get; set; } = 80;
        public string host { get; set; } = "";
        public string userAgent { get; set; } = "";
    }
}
