namespace NGEL.Data.DB
{
    public class DBHelper
    {
        public readonly DBHelperActiveUser ActiveUser;
        public readonly DBHelperAdmin Admin;
        public readonly DBHelperAdminWriteOnly AdminWriteOnly;
        public readonly DBHelperChatLog ChatLog;
        public readonly DBHelperGame Game;
        public readonly DBHelperGameManager GameManager;
        public readonly DBHelperGameManagerWriteOnly GameManagerWriteOnly;
        public readonly DBHelperGameWriteOnly GameWriteOnly;
        public readonly DBHelperGMLog GMLog;
        public readonly DBHelperGMLogWriteOnly GMLogWriteOnly;
        public readonly DBHelperLog Log;
        public readonly DBHelperVisitWriteOnly VisitWriteOnly;
        public DBHelper(
            DBHelperActiveUser activeUser,
            DBHelperAdmin admin,
            DBHelperAdminWriteOnly adminWriteOnly,
            DBHelperChatLog chatLog,
            DBHelperGame game,
            DBHelperGameManager gameManager,
            DBHelperGameManagerWriteOnly gameManagerWriteOnly,
            DBHelperGameWriteOnly gameWriteOnly,
            DBHelperGMLog gMLog,
            DBHelperGMLogWriteOnly gMLogWriteOnly,
            DBHelperLog log,
            DBHelperVisitWriteOnly visitWriteOnly
        )
        {
            ActiveUser = activeUser;
            Admin = admin;
            AdminWriteOnly = adminWriteOnly;
            ChatLog = chatLog;
            Game = game;
            GameManager = gameManager;
            GameManagerWriteOnly = gameManagerWriteOnly;
            GameWriteOnly = gameWriteOnly;
            GMLog = gMLog;
            GMLogWriteOnly = gMLogWriteOnly;
            Log = log;
            VisitWriteOnly = visitWriteOnly;
        }
    }
}
