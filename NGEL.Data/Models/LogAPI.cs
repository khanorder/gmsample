using System;
using System.Collections.Generic;
using System.Text;

namespace NGEL.Data.Models
{

    public class LogBiskitLogsResponses : IAPIResponse
    {
        public bool result { get; set; } = false;
        public Errors error { get; set; } = Errors.None;
        public SignInUser? user { get; set; } = null;
        public List<BiskitLog> biskitLogs { get; set; } = new List<BiskitLog>();
    }

}
