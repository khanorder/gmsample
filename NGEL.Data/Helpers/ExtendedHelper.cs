using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NGEL.Data;
using NGEL.Data.Models;
using MessagePack;
using NGEL.Data.Vars;
using Org.BouncyCastle.Bcpg;
using Base62;

namespace NGEL.Data.Helpers
{
    public static partial class ExtendedHelper
    {
        public static string GetServerAddress(this HttpContext context)
        {
            if (null == context)
                return "";

            var headerServerAddress = context.Request.Headers["ServerAddress"];
            if (false == string.IsNullOrWhiteSpace(headerServerAddress))
                return headerServerAddress;

            if (null == context.Connection || null == context.Connection.LocalIpAddress)
                return "";

            return context.Connection.LocalIpAddress.ToString();
        }

        public static string GetRemoteIpAddress(this HttpContext context)
        {
            if (null == context.Connection || null == context.Connection.RemoteIpAddress)
                return "";

            return context.Connection.RemoteIpAddress.ToString();
        }

        public static string GetScheme(this HttpContext context)
        {
            return context.Request.Scheme;
        }

        public static int GetPort(this HttpContext context)
        {
            var port = 80;
            try
            {
                if (false == string.IsNullOrWhiteSpace(context.Request.Headers["Port"]))
                {
                    port = int.Parse(context.Request.Headers["Port"]);
                }
                else
                {
                    if (null == context.Request.Host.Port)
                    {
                        port = 0;
                    }
                    else
                    {
                        port = (int)context.Request.Host.Port;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return port;
        }

        public static string GetHost(this HttpContext context)
        {
            var port = context.GetPort();
            if (80 == port || 443 == port)
                return context.Request.Host.Host;

            return context.Request.Host.Host + ":" + port;
        }

        public static string GetClaim(this HttpContext context, string claimType)
        {
            var result = "";

            if (null == context)
                return "";

            if (null == context.User)
                return "";

            if (null == context.User.Claims)
                return "";

            if (1 > context.User.Claims.Count())
                return "";

            try
            {
                var claims = context.User.Claims.ToList();
                var claim = claims.FirstOrDefault(_ => _.Type == claimType);
                if (null == claim)
                    return "";

                result = claim.Value;
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }

        public static List<UserMenu> Clone(this List<UserMenu> menus)
        {
            var temp = new List<UserMenu>();
            if (1 > menus.Count)
                return temp;

            foreach (var menu in menus)
            {
                var clone = menu.Clone();
                if (0 < clone.children.Count)
                    clone.children = clone.children.Clone();
                temp.Add(clone);
            }

            return temp;
        }

        public static List<NavMenu> Clone(this List<NavMenu> menus)
        {
            var temp = new List<NavMenu>();
            if (1 > menus.Count)
                return temp;

            foreach (var menu in menus)
            {
                var clone = menu.Clone();
                if (0 < clone.children.Count)
                    clone.children = clone.children.Clone();
                temp.Add(clone);
            }

            return temp;
        }

        public static List<UserMenu> CloneParents(this List<NavMenu> menus)
        {
            var temp = new List<UserMenu>();
            if (1 > menus.Count)
                return temp;

            foreach (var menu in menus)
            {
                var clone = menu.CloneParents();
                if (0 < menu.children.Count)
                {
                    clone.children = new List<UserMenu>();
                    clone.children.AddRange(menu.children.CloneParents());
                }
                temp.Add(clone);
            }

            return temp;
        }

        public static List<NavMenu> GetAllowed(this List<NavMenu> menus, List<UserRoleName> userRoleNames)
        {
            var allowedes = new List<NavMenu>();
            if (1 > menus.Count)
                return allowedes;

            foreach (var menu in menus)
            {
                if (0 < userRoleNames.Count)
                {
                    foreach (var userRoleName in userRoleNames)
                    {
                        if (1 > menu.roles.Length || menu.roles.Contains(userRoleName.name))
                        {
                            var allowed = menu.Clone();

                            if (0 < menu.children.Count)
                            {
                                allowed.children = new List<NavMenu>();
                                allowed.children.AddRange(menu.children.GetAllowed(userRoleNames));
                            }
                            allowedes.Add(allowed);
                            break;
                        }
                    }
                }
                else if (1 > menu.roles.Length)
                {
                    var allowed = menu.Clone();

                    if (0 < menu.children.Count)
                    {
                        allowed.children = new List<NavMenu>();
                        allowed.children.AddRange(menu.children.GetAllowed(userRoleNames));
                    }
                    allowedes.Add(allowed);
                }
            }

            return allowedes;
        }

        public static List<UserMenu> GetAllowedUserMenu(this List<NavMenu> menus, List<UserRoleName> userRoleNames)
        {
            var allowedes = new List<UserMenu>();
            if (1 > menus.Count)
                return allowedes;

            foreach (var menu in menus)
            {
                if (0 < userRoleNames.Count)
                {
                    foreach (var userRoleName in userRoleNames)
                    {
                        if (1 > menu.roles.Length || menu.roles.Contains(userRoleName.name))
                        {
                            var allowed = menu.CloneParents();

                            if (0 < menu.children.Count)
                            {
                                allowed.children = new List<UserMenu>();
                                allowed.children.AddRange(menu.children.GetAllowedUserMenu(userRoleNames));
                            }
                            allowedes.Add(allowed);
                            break;
                        }
                    }
                }
                else if (1 > menu.roles.Length)
                {
                    var allowed = menu.CloneParents();

                    if (0 < menu.children.Count)
                    {
                        allowed.children = new List<UserMenu>();
                        allowed.children.AddRange(menu.children.GetAllowedUserMenu(userRoleNames));
                    }
                    allowedes.Add(allowed);
                }
            }

            return allowedes;
        }

        public static List<NavMenu> GetForManage(this List<NavMenu> menus)
        {
            var forManage = new List<NavMenu>();
            if (1 > menus.Count)
                return forManage;

            foreach (var menu in menus)
            {
                if (1 > menu.roles.Length || "관리자 메뉴" == menu.name || "Administrator" == menu.policyName)
                    continue;

                var clone = menu.Clone();
                clone.children = new List<NavMenu>();
                clone.children.AddRange(menu.children.GetForManage());
                forManage.Add(clone);
            }

            return forManage;
        }

        public static ServiceVersionManagementInfo GetServiceVersionManagementInfo(this VersionInfo data)
        {
            return new ServiceVersionManagementInfo
            {
                Version = data.version,
                Platform = data.platform,
                ServerState = data.serverState,
                ClientState = data.clientState,
                CDNInfo = data.cdnInfo
            };
        }

        public static string? FirstCharToLowerCase(this string? str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        public static string GetTimeZoneString(this Defines.TimeZone timeZone)
        {
            return GetTimeZoneString(GetTimeZoneInfo(timeZone));
        }

        public static string GetTimeZoneString(TimeZoneInfo timeZoneInfo)
        {
            var stName = timeZoneInfo.BaseUtcOffset.TotalHours.ToString();
            switch (timeZoneInfo.BaseUtcOffset.TotalHours)
            {
                case 9:
                    stName = "한국";
                    break;

                case -8:
                    stName = "북미";
                    break;
            }
            return stName;
        }

        public static TimeZoneInfo GetTimeZoneInfo(Defines.TimeZone timeZone)
        {
            switch (timeZone)
            {
                case Defines.TimeZone.UTC:
                    return TimeZoneInfo.Utc;

                case Defines.TimeZone.KST:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        return TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
                    }
                    else
                    {
                        return TimeZoneInfo.FindSystemTimeZoneById("Asia/Seoul");
                    }

                case Defines.TimeZone.PST:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        return TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                    }
                    else
                    {
                        return TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");
                    }
            }

            return TimeZoneInfo.Local;
        }

        public static DateTime ConvertTimeZone(this DateTime dateTime, Defines.TimeZone toTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc, GetTimeZoneInfo(toTimeZone));
        }

        public static DateTime ConvertTimeZone(this DateTime dateTime, TimeZoneInfo toTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc, toTimeZoneInfo);
        }

        public static DateTime ConvertTimeZone(this DateTime dateTime, Defines.TimeZone fromTimeZone, Defines.TimeZone toTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, GetTimeZoneInfo(fromTimeZone), GetTimeZoneInfo(toTimeZone));
        }

        public static DateTime ConvertTimeZone(this DateTime dateTime, TimeZoneInfo fromTimeZoneInfo, TimeZoneInfo toTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(dateTime, fromTimeZoneInfo, toTimeZoneInfo);
        }

        public static Errors SerializeMessagePack<T>(this T data, out string messagePack)
        {
            messagePack = "";

            try
            {
                var convertBytes = MessagePackSerializer.Serialize(data);
                if (null == convertBytes || 1 > convertBytes.Length)
                    return Errors.Common_FailedToEncodeMsgPack;

                messagePack = Convert.ToBase64String(convertBytes);
                return Errors.None;
            }
            catch
            {
                return Errors.Common_FailedToEncodeMsgPack;
            }
        }

        public static Errors DeserializeMessagePack<T>(this string base64Packet, out T? packet)
        {
            packet = default;

            try
            {
                var packetBytes = Convert.FromBase64String(base64Packet);
                if (null == packetBytes)
                    return Errors.Common_PacketArgsEmpty;

                packet = MessagePackSerializer.Deserialize<T>(packetBytes);
            }
            catch
            {
                return Errors.Common_FailedToDecodeMsgPack;
            }

            if (null == packet)
            {
                return Errors.Common_PacketNull;
            }
            else
            {
                return Errors.None;
            }
        }

        public static GMUser GetGMUser(this OAuthSignInUser user)
        {
            return new GMUser
            {
                userId = user.userId,
                token = user.token,
                name = user.name,
                countFailedSignin = user.countFailedSignin,
                latestSignin = user.latestSignin,
                latestSignout = user.latestSignout,
                emailId = user.emailId,
                email = user.email,
                updatedTime = user.updatedTime,
                userJobs = new Dictionary<Guid, UserJob>()
            };
        }

        public static Errors TryToBase62(this Guid id, out string base62)
        {
            base62 = "";

            try
            {
                var bytes = id.ToByteArray();
                base62 = bytes.ToBase62();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return Errors.Common_FailedToParse;
            }

            if (string.IsNullOrWhiteSpace(base62))
            {
                return Errors.Common_FailedToParse;
            }
            else
            {
                return Errors.None;
            }
        }

        public static string? ToBase62(this Guid id)
        {
            string? base62 = "";

            if (Guid.Empty == id)
                return null;

            try
            {
                var bytes = id.ToByteArray();
                base62 = bytes.ToBase62();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }

            return base62;
        }

        public static Errors TryToGuid(this string guidString, out Guid id)
        {
            id = Guid.Empty;

            if (string.IsNullOrWhiteSpace(guidString))
                return Errors.Common_FailedToParse;

            try
            {

                switch (guidString.Length)
                {
                    case 22:
                        var bytes = guidString.FromBase62();
                        id = new Guid(bytes);
                        break;

                    case 36:
                        id = new Guid(guidString);
                        break;

                    default:
                        return Errors.Common_FailedToParse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return Errors.Common_FailedToParse;
            }

            if (Guid.Empty == id)
            {
                return Errors.Common_FailedToParse;
            }
            else
            {
                return Errors.None;
            }
        }

        public static Guid? ToGuid(this string guidString)
        {
            Guid? id = null;

            if (string.IsNullOrWhiteSpace(guidString))
                return null;

            try
            {
                switch (guidString.Length)
                {
                    case 22:
                        var bytes = guidString.FromBase62();
                        id = new Guid(bytes);
                        break;

                    case 36:
                        id = new Guid(guidString);
                        break;

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }

            return id;
        }
    }
}
