namespace GMServer.Models
{
    public class LineNotifyAuthResult
    {
        public string code { get; set; }
        public string state { get; set; }
    }

    public class LineNotifyTokenResult
    {
        public string access_token { get; set; }
    }

    public class LineNotifyResult
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
