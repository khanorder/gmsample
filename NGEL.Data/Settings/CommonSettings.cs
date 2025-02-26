using System;
using System.Collections.Generic;
using NGEL.Data;
using NGEL.Data.Models;

namespace NGEL.Data.Settings
{
    public class CommonSettings
    {
        public string companyName { get; set; } = "";
        public string companyNameEn { get; set; } = "";
        public string projectName { get; set; } = "";
        public string projectNameEn { get; set; } = "";
        public string cookieName { get; set; } = "";
        public string uploadFolderName { get; set; } = "";
        public string packageName { get; set; } = "";
        public string[] superEmails { get; set; } = new string[] { "zanghobae@gmail.com" };
        public int authPersistence { get; set; } = 30;
        public Defines.ServerStateType serverState { get; set; } = Defines.ServerStateType.InsideTest;
        public List<DatabaseConnection> databases { get; set; } = new List<DatabaseConnection>();
        public List<ClientInfo> clientInfo { get; set; } = new List<ClientInfo>();
        public StompInfo stompInfo { get; set; } = new StompInfo();
        public string cryptoKey { get; set; } = "";
        public string cryptoIV { get; set; } = "";
        public Int64 countMaxDBData { get; set; } = 100000;
        public Int64 countMaxDownloadDBData { get; set; } = 100000;
        public bool isService {
            get {
                return Defines.ServerStateType.Service == this.serverState;
            } 
        }
    }
}
