using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using NPOI.SS.UserModel;
using Lobby;

namespace NGEL.Data.Models
{
    [MessagePackObject]
    public class Server
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("isMaster")]
        public bool isMaster { get; set; } = false;
        [Key("updateTime")]
        public DateTime updateTime { get; set; } = DateTime.UtcNow;
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;

        public Server Clone()
        {
            var clone = new Server();
            clone.id = this.id;
            clone.isMaster = this.isMaster;
            clone.updateTime = this.updateTime;
            clone.regTime = this.regTime;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(Server rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class DatabaseConnection
    {
        [Key("dbRole")]
        public Defines.DatabaseRole dbRole { get; set; } = 0;
        [Key("dbType")]
        public Defines.DatabaseType dbType { get; set; } = 0;
        [Key("connectionString")]
        public string connectionString { get; set; } = "";

        public DatabaseConnection Clone()
        {
            var clone = new DatabaseConnection();
            clone.dbRole = this.dbRole;
            clone.dbType = this.dbType;
            clone.connectionString = this.connectionString;
            return clone;
        }

        public bool CompareKey(Defines.DatabaseRole dbRole)
        {
           return this.dbRole == dbRole;
        }

        public bool CompareKey(DatabaseConnection rdata)
        {
           return dbRole == rdata.dbRole;
        }
    }

    [MessagePackObject]
    public class ClientInfo
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("host")]
        public string host { get; set; } = "";

        public ClientInfo Clone()
        {
            var clone = new ClientInfo();
            clone.id = this.id;
            clone.host = this.host;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(ClientInfo rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class StompInfo
    {
        [Key("brokerUrls")]
        public List<string> brokerUrls { get; set; } = new List<string>();
        [Key("login")]
        public string login { get; set; } = "";
        [Key("passcode")]
        public string passcode { get; set; } = "";

        public StompInfo Clone()
        {
            var clone = new StompInfo();
            clone.brokerUrls.AddRange(this.brokerUrls);
            clone.login = this.login;
            clone.passcode = this.passcode;
            return clone;
        }
    }

    [MessagePackObject]
    public class UserMenu
    {
        [Key("name")]
        public string name { get; set; } = "";
        [Key("path")]
        public string path { get; set; } = "";
        [Key("parameters")]
        public Dictionary<string, string> parameters { get; set; } = new Dictionary<string, string>();
        [Key("children")]
        public List<UserMenu> children { get; set; } = new List<UserMenu>();
        [Key("opened")]
        public bool opened { get; set; } = false;
        [Key("active")]
        public bool active { get; set; } = false;

        public UserMenu Clone()
        {
            var clone = new UserMenu();
            clone.name = this.name;
            clone.path = this.path;
            clone.parameters = new Dictionary<string, string>();
            foreach (var key in this.parameters.Keys)
            {
                if (this.parameters.TryGetValue(key, out var value))
                    clone.parameters.Add(key, value);
            }
            clone.children.AddRange(this.children);
            clone.opened = this.opened;
            clone.active = this.active;
            return clone;
        }

        public bool CompareKey(string name)
        {
           return this.name == name;
        }

        public bool CompareKey(UserMenu rdata)
        {
           return name == rdata.name;
        }
    }

    [MessagePackObject]
    public class NavMenu
    {
        [Key("name")]
        public string name { get; set; } = "";
        [Key("path")]
        public string path { get; set; } = "";
        [Key("parameters")]
        public Dictionary<string, string> parameters { get; set; } = new Dictionary<string, string>();
        [Key("policyName")]
        public string policyName { get; set; } = "";
        [Key("roles")]
        public string[] roles { get; set; } = Array.Empty<string>();
        [Key("children")]
        public List<NavMenu> children { get; set; } = new List<NavMenu>();

        public NavMenu Clone()
        {
            var clone = new NavMenu();
            clone.name = this.name;
            clone.path = this.path;
            clone.parameters = new Dictionary<string, string>();
            foreach (var key in this.parameters.Keys)
            {
                if (this.parameters.TryGetValue(key, out var value))
                    clone.parameters.Add(key, value);
            }
            clone.policyName = this.policyName;
            clone.roles = new string[this.roles.Length];
            this.roles.CopyTo(clone.roles, 0);
            clone.children.AddRange(this.children);
            return clone;
        }

        public bool CompareKey(string name)
        {
           return this.name == name;
        }

        public bool CompareKey(NavMenu rdata)
        {
           return name == rdata.name;
        }

        public UserMenu CloneParents()
        {
            var clone = new UserMenu();
            clone.name = this.name;
            clone.path = this.path;
            foreach (var item in this.parameters)
            {
                clone.parameters.Append(item);
            }
            return clone;
        }
    }

    [MessagePackObject]
    public class User
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("password")]
        public string password { get; set; } = "";
        [Key("name")]
        public string name { get; set; } = "";
        [Key("isDeleted")]
        public bool isDeleted { get; set; } = false;
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("latestChangePW")]
        public DateTime latestChangePW { get; set; } = DateTime.UtcNow;
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;

        public User Clone()
        {
            var clone = new User();
            clone.id = this.id;
            clone.password = this.password;
            clone.name = this.name;
            clone.isDeleted = this.isDeleted;
            clone.countFailedSignin = this.countFailedSignin;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.regTime = this.regTime;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(User rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserEmail
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("email")]
        public string email { get; set; } = "";
        [Key("emailConfirmId")]
        public string emailConfirmId { get; set; } = "";
        [Key("isEmailConfirmed")]
        public bool isEmailConfirmed { get; set; } = false;
        [Key("emailConfirmIdIssuedTime")]
        public DateTime emailConfirmIdIssuedTime { get; set; } = DateTime.UtcNow;
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;

        public UserEmail Clone()
        {
            var clone = new UserEmail();
            clone.id = this.id;
            clone.email = this.email;
            clone.emailConfirmId = this.emailConfirmId;
            clone.isEmailConfirmed = this.isEmailConfirmed;
            clone.emailConfirmIdIssuedTime = this.emailConfirmIdIssuedTime;
            clone.userId = this.userId;
            return clone;
        }

        public bool CompareKey(Guid id, string email)
        {
           return this.id == id
                && this.email == email;
        }

        public bool CompareKey(UserEmail rdata)
        {
           return id == rdata.id
                && email == rdata.email;
        }
    }

    [MessagePackObject]
    public class UserOAuthKey
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("provider")]
        public Defines.OAuthProvider provider { get; set; } = 0;
        [Key("providerUid")]
        public string providerUid { get; set; } = "";
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("userEmailId")]
        public Guid userEmailId { get; set; } = Guid.Empty;

        public UserOAuthKey Clone()
        {
            var clone = new UserOAuthKey();
            clone.id = this.id;
            clone.provider = this.provider;
            clone.providerUid = this.providerUid;
            clone.userId = this.userId;
            clone.userEmailId = this.userEmailId;
            return clone;
        }

        public bool CompareKey(Guid id, Defines.OAuthProvider provider, string providerUid)
        {
           return this.id == id
                && this.provider == provider
                && this.providerUid == providerUid;
        }

        public bool CompareKey(UserOAuthKey rdata)
        {
           return id == rdata.id
                && provider == rdata.provider
                && providerUid == rdata.providerUid;
        }
    }

    [MessagePackObject]
    public class SignInUser
    {
        [Key("signinId")]
        public Guid signinId { get; set; } = Guid.Empty;
        [Key("name")]
        public string name { get; set; } = "";
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("latestChangePW")]
        public DateTime latestChangePW { get; set; } = DateTime.UtcNow;
        [Key("email")]
        public string email { get; set; } = "";
        [Key("emailConfirmId")]
        public string emailConfirmId { get; set; } = "";
        [Key("isEmailConfirmed")]
        public bool isEmailConfirmed { get; set; } = false;
        [Key("provider")]
        public Defines.OAuthProvider provider { get; set; } = 0;
        [Key("updatedTime")]
        public DateTime updatedTime { get; set; } = DateTime.UtcNow;
        [Key("menus")]
        public List<UserMenu> menus { get; set; } = new List<UserMenu>();
        [Key("menusLinear")]
        public List<UserMenu> menusLinear { get; set; } = new List<UserMenu>();
        [Key("roles")]
        public List<string> roles { get; set; } = new List<string>();

        public SignInUser Clone()
        {
            var clone = new SignInUser();
            clone.signinId = this.signinId;
            clone.name = this.name;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.email = this.email;
            clone.emailConfirmId = this.emailConfirmId;
            clone.isEmailConfirmed = this.isEmailConfirmed;
            clone.provider = this.provider;
            clone.updatedTime = this.updatedTime;
            clone.menus.AddRange(this.menus);
            clone.menusLinear.AddRange(this.menusLinear);
            clone.roles.AddRange(this.roles);
            return clone;
        }

        public bool CompareKey(Guid signinId)
        {
           return this.signinId == signinId;
        }

        public bool CompareKey(SignInUser rdata)
        {
           return signinId == rdata.signinId;
        }
    }

    [MessagePackObject]
    public class OAuthSignInUser
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("signinId")]
        public Guid signinId { get; set; } = Guid.Empty;
        [Key("token")]
        public string token { get; set; } = "";
        [Key("name")]
        public string name { get; set; } = "";
        [Key("isDeleted")]
        public bool isDeleted { get; set; } = false;
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("latestChangePW")]
        public DateTime latestChangePW { get; set; } = DateTime.UtcNow;
        [Key("emailId")]
        public Guid emailId { get; set; } = Guid.Empty;
        [Key("email")]
        public string email { get; set; } = "";
        [Key("emailConfirmId")]
        public string emailConfirmId { get; set; } = "";
        [Key("isEmailConfirmed")]
        public bool isEmailConfirmed { get; set; } = false;
        [Key("oAuthKeyId")]
        public Guid oAuthKeyId { get; set; } = Guid.Empty;
        [Key("provider")]
        public Defines.OAuthProvider provider { get; set; } = 0;
        [Key("updatedTime")]
        public DateTime updatedTime { get; set; } = DateTime.UtcNow;
        [Key("menus")]
        public List<UserMenu> menus { get; set; } = new List<UserMenu>();

        public OAuthSignInUser Clone()
        {
            var clone = new OAuthSignInUser();
            clone.userId = this.userId;
            clone.signinId = this.signinId;
            clone.token = this.token;
            clone.name = this.name;
            clone.isDeleted = this.isDeleted;
            clone.countFailedSignin = this.countFailedSignin;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.emailId = this.emailId;
            clone.email = this.email;
            clone.emailConfirmId = this.emailConfirmId;
            clone.isEmailConfirmed = this.isEmailConfirmed;
            clone.oAuthKeyId = this.oAuthKeyId;
            clone.provider = this.provider;
            clone.updatedTime = this.updatedTime;
            clone.menus.AddRange(this.menus);
            return clone;
        }

        public bool CompareKey(Guid userId, Guid signinId)
        {
           return this.userId == userId
                && this.signinId == signinId;
        }

        public bool CompareKey(OAuthSignInUser rdata)
        {
           return userId == rdata.userId
                && signinId == rdata.signinId;
        }

        public SignInUser CloneParents()
        {
            var clone = new SignInUser();
            clone.signinId = this.signinId;
            clone.name = this.name;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.email = this.email;
            clone.emailConfirmId = this.emailConfirmId;
            clone.isEmailConfirmed = this.isEmailConfirmed;
            clone.provider = this.provider;
            clone.updatedTime = this.updatedTime;
            clone.menus.AddRange(this.menus);
            return clone;
        }
    }

    [MessagePackObject]
    public class GMUser
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("token")]
        public string token { get; set; } = "";
        [Key("name")]
        public string name { get; set; } = "";
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("emailId")]
        public Guid emailId { get; set; } = Guid.Empty;
        [Key("email")]
        public string email { get; set; } = "";
        [Key("updatedTime")]
        public DateTime updatedTime { get; set; } = DateTime.UtcNow;
        [Key("userJobs")]
        public Dictionary<Guid, UserJob> userJobs { get; set; } = new Dictionary<Guid, UserJob>();

        public GMUser Clone()
        {
            var clone = new GMUser();
            clone.userId = this.userId;
            clone.token = this.token;
            clone.name = this.name;
            clone.countFailedSignin = this.countFailedSignin;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.emailId = this.emailId;
            clone.email = this.email;
            clone.updatedTime = this.updatedTime;
            clone.userJobs = new Dictionary<Guid, UserJob>();
            foreach (var key in this.userJobs.Keys)
            {
                if (this.userJobs.TryGetValue(key, out var value))
                    clone.userJobs.Add(key, value);
            }
            return clone;
        }

        public bool CompareKey(Guid userId)
        {
           return this.userId == userId;
        }

        public bool CompareKey(GMUser rdata)
        {
           return userId == rdata.userId;
        }
    }

    [MessagePackObject]
    public class UserJob
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("jobType")]
        public Defines.UserJobType jobType { get; set; } = 0;
        [Key("jobCount")]
        public UInt64 jobCount { get; set; } = 0;
        [Key("succeededCount")]
        public UInt64 succeededCount { get; set; } = 0;
        [Key("failedCount")]
        public UInt64 failedCount { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
        [Key("startTime")]
        public DateTime startTime { get; set; } = DateTime.UtcNow;
        [Key("updateTime")]
        public DateTime updateTime { get; set; } = DateTime.UtcNow;
        [Key("isOpenMessage")]
        public bool isOpenMessage { get; set; } = false;

        public UserJob Clone()
        {
            var clone = new UserJob();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.jobType = this.jobType;
            clone.jobCount = this.jobCount;
            clone.succeededCount = this.succeededCount;
            clone.failedCount = this.failedCount;
            clone.message = this.message;
            clone.startTime = this.startTime;
            clone.updateTime = this.updateTime;
            clone.isOpenMessage = this.isOpenMessage;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(UserJob rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class Role
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("name")]
        public string name { get; set; } = "";

        public Role Clone()
        {
            var clone = new Role();
            clone.id = this.id;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(Role rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserRole
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("roleId")]
        public Guid roleId { get; set; } = Guid.Empty;

        public UserRole Clone()
        {
            var clone = new UserRole();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.roleId = this.roleId;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(UserRole rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserRoleName
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("roleId")]
        public Guid roleId { get; set; } = Guid.Empty;
        [Key("name")]
        public string name { get; set; } = "";

        public UserRoleName Clone()
        {
            var clone = new UserRoleName();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.roleId = this.roleId;
            clone.name = this.name;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(UserRoleName rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserInfo
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("name")]
        public string name { get; set; } = "";
        [Key("isDeleted")]
        public bool isDeleted { get; set; } = false;
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("latestChangePW")]
        public DateTime latestChangePW { get; set; } = DateTime.UtcNow;
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;
        [Key("emails")]
        public List<string> emails { get; set; } = new List<string>();
        [Key("roles")]
        public List<string> roles { get; set; } = new List<string>();

        public UserInfo Clone()
        {
            var clone = new UserInfo();
            clone.id = this.id;
            clone.name = this.name;
            clone.isDeleted = this.isDeleted;
            clone.countFailedSignin = this.countFailedSignin;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.regTime = this.regTime;
            clone.emails.AddRange(this.emails);
            clone.roles.AddRange(this.roles);
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(UserInfo rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserInfoForManage
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("name")]
        public string name { get; set; } = "";
        [Key("isDeleted")]
        public bool isDeleted { get; set; } = false;
        [Key("countFailedSignin")]
        public int countFailedSignin { get; set; } = 0;
        [Key("latestSignin")]
        public DateTime latestSignin { get; set; } = DateTime.UtcNow;
        [Key("latestSignout")]
        public DateTime latestSignout { get; set; } = DateTime.UtcNow;
        [Key("latestChangePW")]
        public DateTime latestChangePW { get; set; } = DateTime.UtcNow;
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;
        [Key("emails")]
        public List<string> emails { get; set; } = new List<string>();
        [Key("roles")]
        public List<string> roles { get; set; } = new List<string>();
        [Key("emailId")]
        public Guid emailId { get; set; } = Guid.Empty;
        [Key("provider")]
        public Defines.OAuthProvider provider { get; set; } = 0;
        [Key("emailConfirmId")]
        public string emailConfirmId { get; set; } = "";
        [Key("isEmailConfirmed")]
        public bool isEmailConfirmed { get; set; } = false;

        public UserInfoForManage Clone()
        {
            var clone = new UserInfoForManage();
            clone.id = this.id;
            clone.name = this.name;
            clone.isDeleted = this.isDeleted;
            clone.countFailedSignin = this.countFailedSignin;
            clone.latestSignin = this.latestSignin;
            clone.latestSignout = this.latestSignout;
            clone.latestChangePW = this.latestChangePW;
            clone.regTime = this.regTime;
            clone.emails.AddRange(this.emails);
            clone.roles.AddRange(this.roles);
            clone.emailId = this.emailId;
            clone.provider = this.provider;
            clone.emailConfirmId = this.emailConfirmId;
            clone.isEmailConfirmed = this.isEmailConfirmed;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(UserInfoForManage rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class UserInfoForAdd
    {
        [Key("name")]
        public string name { get; set; } = "";
        [Key("email")]
        public string email { get; set; } = "";
        [Key("password")]
        public string password { get; set; } = "";
        [Key("roles")]
        public List<string> roles { get; set; } = new List<string>();

        public UserInfoForAdd Clone()
        {
            var clone = new UserInfoForAdd();
            clone.name = this.name;
            clone.email = this.email;
            clone.password = this.password;
            clone.roles.AddRange(this.roles);
            return clone;
        }
    }

    [MessagePackObject]
    public class UserSignin
    {
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("signinId")]
        public Guid signinId { get; set; } = Guid.Empty;
        [Key("connectionId")]
        public string connectionId { get; set; } = "";
        [Key("latestUpdate")]
        public DateTime latestUpdate { get; set; } = DateTime.UtcNow;
        [Key("latestActive")]
        public DateTime latestActive { get; set; } = DateTime.UtcNow;

        public UserSignin Clone()
        {
            var clone = new UserSignin();
            clone.userId = this.userId;
            clone.signinId = this.signinId;
            clone.connectionId = this.connectionId;
            clone.latestUpdate = this.latestUpdate;
            clone.latestActive = this.latestActive;
            return clone;
        }

        public bool CompareKey(Guid userId)
        {
           return this.userId == userId;
        }

        public bool CompareKey(UserSignin rdata)
        {
           return userId == rdata.userId;
        }
    }

    [MessagePackObject]
    public class UserPassword
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("userId")]
        public Guid userId { get; set; } = Guid.Empty;
        [Key("password")]
        public string password { get; set; } = "";
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;

        public UserPassword Clone()
        {
            var clone = new UserPassword();
            clone.id = this.id;
            clone.userId = this.userId;
            clone.password = this.password;
            clone.regTime = this.regTime;
            return clone;
        }

        public bool CompareKey(Guid id, Guid userId, string password)
        {
           return this.id == id
                && this.userId == userId
                && this.password == password;
        }

        public bool CompareKey(UserPassword rdata)
        {
           return id == rdata.id
                && userId == rdata.userId
                && password == rdata.password;
        }
    }

    [MessagePackObject]
    public class ServiceVersionManagementInfo
    {
        [Key("Version")]
        public string Version { get; set; } = "";
        [Key("Platform")]
        public Defines.ServiceVersionManagementPlatform Platform { get; set; } = 0;
        [Key("ServerState")]
        public Defines.ServiceVersionManagementServerState ServerState { get; set; } = 0;
        [Key("ClientState")]
        public Defines.ServiceVersionManagementClientState ClientState { get; set; } = 0;
        [Key("CDNInfo")]
        public string CDNInfo { get; set; } = "";

        public ServiceVersionManagementInfo Clone()
        {
            var clone = new ServiceVersionManagementInfo();
            clone.Version = this.Version;
            clone.Platform = this.Platform;
            clone.ServerState = this.ServerState;
            clone.ClientState = this.ClientState;
            clone.CDNInfo = this.CDNInfo;
            return clone;
        }

        public bool CompareKey(string Version, Defines.ServiceVersionManagementPlatform Platform)
        {
           return this.Version == Version
                && this.Platform == Platform;
        }

        public bool CompareKey(ServiceVersionManagementInfo rdata)
        {
           return Version == rdata.Version
                && Platform == rdata.Platform;
        }
    }

    [MessagePackObject]
    public class VersionInfo
    {
        [Key("version")]
        public string version { get; set; } = "";
        [Key("platform")]
        public Defines.ServiceVersionManagementPlatform platform { get; set; } = 0;
        [Key("serverState")]
        public Defines.ServiceVersionManagementServerState serverState { get; set; } = 0;
        [Key("clientState")]
        public Defines.ServiceVersionManagementClientState clientState { get; set; } = 0;
        [Key("cdnInfo")]
        public string cdnInfo { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public VersionInfo Clone()
        {
            var clone = new VersionInfo();
            clone.version = this.version;
            clone.platform = this.platform;
            clone.serverState = this.serverState;
            clone.clientState = this.clientState;
            clone.cdnInfo = this.cdnInfo;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(string version, Defines.ServiceVersionManagementPlatform platform)
        {
           return this.version == version
                && this.platform == platform;
        }

        public bool CompareKey(VersionInfo rdata)
        {
           return version == rdata.version
                && platform == rdata.platform;
        }
    }

    [MessagePackObject]
    public class ChattingNotice
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("message")]
        public string message { get; set; } = "";
        [Key("noticeType")]
        public ENoticeType noticeType { get; set; } = 0;
        [Key("visibleTime")]
        public int visibleTime { get; set; } = 0;
        [Key("visibleCount")]
        public int visibleCount { get; set; } = 0;
        [Key("noticeTime")]
        public DateTime noticeTime { get; set; } = DateTime.UtcNow;
        [Key("regTime")]
        public DateTime regTime { get; set; } = DateTime.UtcNow;
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public ChattingNotice Clone()
        {
            var clone = new ChattingNotice();
            clone.id = this.id;
            clone.message = this.message;
            clone.noticeType = this.noticeType;
            clone.visibleTime = this.visibleTime;
            clone.visibleCount = this.visibleCount;
            clone.noticeTime = this.noticeTime;
            clone.regTime = this.regTime;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(ChattingNotice rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class BlockContent
    {
        [Key("packetID")]
        public string packetID { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public BlockContent Clone()
        {
            var clone = new BlockContent();
            clone.packetID = this.packetID;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(string packetID)
        {
           return this.packetID == packetID;
        }

        public bool CompareKey(BlockContent rdata)
        {
           return packetID == rdata.packetID;
        }
    }

    [MessagePackObject]
    public class WhiteListData
    {
        [Key("whiteIP")]
        public string whiteIP { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public WhiteListData Clone()
        {
            var clone = new WhiteListData();
            clone.whiteIP = this.whiteIP;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(string whiteIP)
        {
           return this.whiteIP == whiteIP;
        }

        public bool CompareKey(WhiteListData rdata)
        {
           return whiteIP == rdata.whiteIP;
        }
    }

    [MessagePackObject]
    public class Firewall
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("method")]
        public Defines.FirewallMethod method { get; set; } = 0;
        [Key("ipAddress")]
        public string ipAddress { get; set; } = "";
        [Key("description")]
        public string description { get; set; } = "";
        [Key("isNewData")]
        public bool isNewData { get; set; } = false;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Firewall Clone()
        {
            var clone = new Firewall();
            clone.id = this.id;
            clone.method = this.method;
            clone.ipAddress = this.ipAddress;
            clone.description = this.description;
            clone.isNewData = this.isNewData;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(Firewall rdata)
        {
           return id == rdata.id;
        }
    }

    [MessagePackObject]
    public class CCU
    {
        [Key("ID")]
        public UInt64 ID { get; set; } = 0;
        [Key("LobbyID")]
        public string LobbyID { get; set; } = "";
        [Key("CreateAt")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Key("Total")]
        public UInt64 Total { get; set; } = 0;

        public CCU Clone()
        {
            var clone = new CCU();
            clone.ID = this.ID;
            clone.LobbyID = this.LobbyID;
            clone.CreateAt = this.CreateAt;
            clone.Total = this.Total;
            return clone;
        }

        public bool CompareKey(UInt64 ID)
        {
           return this.ID == ID;
        }

        public bool CompareKey(CCU rdata)
        {
           return ID == rdata.ID;
        }
    }

    [MessagePackObject]
    public class ManagerServerLog
    {
        [Key("logData")]
        public string logData { get; set; } = "";
        [Key("logTime")]
        public DateTime? logTime { get; set; } = null;
        [Key("logType")]
        public string logType { get; set; } = "";

        public ManagerServerLog Clone()
        {
            var clone = new ManagerServerLog();
            clone.logData = this.logData;
            clone.logTime = this.logTime;
            clone.logType = this.logType;
            return clone;
        }
    }

    [MessagePackObject]
    public class Settings
    {
        [Key("key")]
        public string key { get; set; } = "";
        [Key("value")]
        public int value { get; set; } = 0;
        [Key("isChanged")]
        public bool isChanged { get; set; } = false;

        public Settings Clone()
        {
            var clone = new Settings();
            clone.key = this.key;
            clone.value = this.value;
            clone.isChanged = this.isChanged;
            return clone;
        }

        public bool CompareKey(string key)
        {
           return this.key == key;
        }

        public bool CompareKey(Settings rdata)
        {
           return key == rdata.key;
        }
    }

    [MessagePackObject]
    public class ChattingMessage
    {
        [Key("id")]
        public Guid id { get; set; } = Guid.Empty;
        [Key("messageType")]
        public Defines.ChattingMessageType messageType { get; set; } = 0;
        [Key("message")]
        public string message { get; set; } = "";
        [Key("sendSigninId")]
        public Guid sendSigninId { get; set; } = Guid.Empty;
        [Key("sendUserName")]
        public string sendUserName { get; set; } = "";
        [Key("sendConnectionId")]
        public string sendConnectionId { get; set; } = "";
        [Key("targetUserId")]
        public Guid targetUserId { get; set; } = Guid.Empty;
        [Key("targetConnectionId")]
        public string targetConnectionId { get; set; } = "";
        [Key("localSendTime")]
        public DateTime? localSendTime { get; set; } = null;
        [Key("localReceiveTime")]
        public DateTime? localReceiveTime { get; set; } = null;
        [Key("serverReceiveTime")]
        public DateTime? serverReceiveTime { get; set; } = null;
        [Key("serverSendTime")]
        public DateTime? serverSendTime { get; set; } = null;
        [Key("isRead")]
        public bool isRead { get; set; } = false;

        public ChattingMessage Clone()
        {
            var clone = new ChattingMessage();
            clone.id = this.id;
            clone.messageType = this.messageType;
            clone.message = this.message;
            clone.sendSigninId = this.sendSigninId;
            clone.sendUserName = this.sendUserName;
            clone.sendConnectionId = this.sendConnectionId;
            clone.targetUserId = this.targetUserId;
            clone.targetConnectionId = this.targetConnectionId;
            clone.localSendTime = this.localSendTime;
            clone.localReceiveTime = this.localReceiveTime;
            clone.serverReceiveTime = this.serverReceiveTime;
            clone.serverSendTime = this.serverSendTime;
            clone.isRead = this.isRead;
            return clone;
        }

        public bool CompareKey(Guid id)
        {
           return this.id == id;
        }

        public bool CompareKey(ChattingMessage rdata)
        {
           return id == rdata.id;
        }
    }

}
