using GMServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using NGEL.Data.Settings;
using System.Security.Claims;
using System.Security.Principal;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NGEL.Data.DB;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NGEL.Data.Helpers;
using System.IO;
using System.Text.RegularExpressions;

namespace GMServer.Filters
{
    public class GMAsyncActionFilter: IAsyncActionFilter
    {
        private readonly ILogger<GMAsyncActionFilter> _logger;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        private readonly FirewallService _firewallService;
        private readonly RequestDataService _requestDataService;
        public GMAsyncActionFilter(ILogger<GMAsyncActionFilter> logger, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment, FirewallService firewallService, RequestDataService requestDataService)
        {
            _logger = logger;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
            _firewallService = firewallService;
            _requestDataService = requestDataService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                if (0 < _firewallService.allowIPAddresses.Count)
                {
                    bool isAllowed = false; 
                    foreach (var allowIP in  _firewallService.allowIPAddresses)
                    {
                        if (string.IsNullOrWhiteSpace(allowIP))
                            continue;

                        var pattern = "^" + allowIP.Replace("[", "\\[").Replace("]", "\\]").Replace(".", "\\.").Replace("*", ".*") + "$";
                        isAllowed = Regex.IsMatch(_requestDataService.remoteAddress, pattern);

                        if (isAllowed)
                            break;
                    }

                    if (false == isAllowed)
                    {
                        if (_environment.IsDevelopment())
                            _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: not allowed ip.");

                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }
                }

                if (0 < _firewallService.denyIPAddresses.Count)
                {
                    foreach (var denyIP in _firewallService.denyIPAddresses)
                    {
                        if (string.IsNullOrWhiteSpace(denyIP))
                            continue;

                        var pattern = "^" + denyIP.Replace("[", "\\[").Replace("]", "\\]").Replace(".", "\\.").Replace("*", ".*") + "$";
                        if (Regex.IsMatch(_requestDataService.remoteAddress, pattern))
                        {
                            if (_environment.IsDevelopment())
                                _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: denied ip.");

                            context.Result = new RedirectResult("/Forbidden");
                            return;
                        }
                    }
                }

                var httpContext = context.HttpContext;

                ClaimsPrincipal? user = null;
                IIdentity? identity = null;
                List<Claim>? claims = null;
                string path = "";

                if (null != httpContext.User)
                {
                    user = httpContext.User;
                    identity = httpContext.User?.Identity;
                    claims = httpContext.User?.Claims?.ToList();
                    path = httpContext.Request.Path;
                }

                // Do something before the action executes.
                if (_environment.IsDevelopment())
                    _logger.LogDebug($"[BEFORE] actionFilter: path - {path}, user - ({identity?.IsAuthenticated}){identity?.Name}", ConsoleColor.Cyan);

                if (null != identity && identity.IsAuthenticated)
                {
                    if (null == claims || 1 > claims.Count())
                    {
                        _logger.LogInformation($"[{(path)}]: claims are empty.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var userIdClaim = httpContext.GetClaim("userId");
                    if (string.IsNullOrWhiteSpace(userIdClaim))
                    {
                        _logger.LogInformation($"[{(path)}]: not found userId claim.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var userId = Guid.Empty;
                    try
                    {
                        userId = Guid.Parse(userIdClaim);
                    }
                    catch
                    {
                        _logger.LogInformation($"[{(path)}]: userId is empty.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var signinIdClaim = httpContext.GetClaim("signinId");
                    if (string.IsNullOrWhiteSpace(signinIdClaim))
                    {
                        _logger.LogInformation($"[{(path)}]: not found signinId claim.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var signinId = Guid.Empty;
                    try
                    {
                        signinId = Guid.Parse(signinIdClaim);
                    }
                    catch
                    {
                        _logger.LogInformation($"[{(path)}]: signinId is empty.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var keyIdClaim = httpContext.GetClaim("keyId");
                    if (string.IsNullOrWhiteSpace(keyIdClaim))
                    {
                        _logger.LogInformation($"[{(path)}]: not found keyId claim.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var keyId = Guid.Empty;
                    try
                    {
                        keyId = Guid.Parse(keyIdClaim);
                    }
                    catch
                    {
                        _logger.LogInformation($"[{(path)}]: keyId is empty.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var updateTimeClaim = httpContext.GetClaim("updateTime");
                    if (string.IsNullOrWhiteSpace(updateTimeClaim))
                    {
                        _logger.LogInformation($"[{(path)}]: not found updateTime claim.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    DateTime? updateTime = null;
                    try
                    {
                        updateTime = DateTime.Parse(updateTimeClaim);
                    }
                    catch
                    {
                        _logger.LogInformation($"[{(path)}]: failed to parse updateTime claim.");
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    var claimUpdateDiff = DateTime.UtcNow.Subtract((DateTime)updateTime);
                    if (claimUpdateDiff.TotalMinutes > _commonSettings.authPersistence)
                    {
                        _logger.LogInformation($"[{(path)}]: the user authentication data was expired.");
                        await _gmAuthentication.SignOutAsync(signinId, userId);
                        context.Result = new RedirectResult("/Forbidden");
                        return;
                    }

                    //var signedInUser = _gmAuthentication.GetSignedInUsers().Find(_ => _.userId.ToString() == userIdClaim);

                    //if (null == signedInUser)
                    //{
                    //    var signInRefreshTask = await _gmAuthentication.SignInRefreshAsync(signinId, keyId);
                    //    if (false == signInRefreshTask.Item1 || null == signInRefreshTask.Item2)
                    //    {
                    //        _logger.LogError($"[{(path)}]: failed to signin refresh.");
                    //        await _gmAuthentication.SignOutAsync(signinId, userId);
                    //        context.Result = new RedirectResult("/Forbidden");
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    if (signedInUser.signinId.ToString() != signinIdClaim)
                    //    {
                    //        _logger.LogInformation($"[{(path)}]: the user was sign in from another device.");
                    //        await _gmAuthentication.SignOutAsync(signinId, userId);
                    //        context.Result = new RedirectResult("/Forbidden");
                    //        return;
                    //    }
                    //}

                    //await _gmAuthentication.UpdateSignInAsync(signinId);
                }
                else
                {
                    if (_environment.IsDevelopment())
                        _logger.LogInformation($"[{(path)}]: Not found any claims.");
                }

                await next();

                // Do something after the action executes.
                if (_environment.IsDevelopment())
                    _logger.LogDebug($"[AFTER] actionFilter: path - {path}, user - ({identity?.IsAuthenticated}){identity?.Name}", ConsoleColor.Cyan);
            }
            catch (Exception ex)
            {
                _logger.LogError($"filtering: {ex.Message}");
                _logger.LogError($"filtering: {ex.StackTrace}");
                context.Result = new RedirectResult("/Forbidden");
                return;
            }
        }
    }
}
