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
using Microsoft.AspNetCore.SignalR;
using GMServer.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using NGEL.Data;
using NGEL.Data.Vars;
using Newtonsoft.Json;

namespace GMServer.Filters
{
    public class GMServerHubFilter : IHubFilter
    {
        private readonly ILogger<GMServerHubFilter> _logger;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RequestDataService _requestDataService;
        private readonly FirewallService _firewallService;
        private readonly AutoGMServerHubSend _hubSend;

        public GMServerHubFilter(ILogger<GMServerHubFilter> logger, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment webHostEnvironment, RequestDataService requestDataService, FirewallService firewallService, AutoGMServerHubSend hubSend)
        {
            _logger = logger;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _webHostEnvironment = webHostEnvironment;
            _requestDataService = requestDataService;
            _firewallService = firewallService;
            _hubSend = hubSend;
        }

        public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"Calling hub method '{invocationContext.HubMethodName}({invocationContext.Context.ConnectionId})'");

            try
            {
                var context = invocationContext.Context.GetHttpContext();

                if (null == context)
                    return new object { };

                var headerServerAddress = context.Request.Headers["ServerAddress"];
                if (string.IsNullOrWhiteSpace(headerServerAddress))
                {
                    var localIpAddress = context.Connection.LocalIpAddress;
                    if (null != localIpAddress)
                        _requestDataService.serverAddress = localIpAddress.ToString();
                }
                else
                {
                    _requestDataService.serverAddress = headerServerAddress.ToString();
                }

                var headerX_Forwarded_For = context.Request.Headers["X-Forwarded-For"];
                if (string.IsNullOrWhiteSpace(headerX_Forwarded_For))
                {
                    if (null != context.Connection.RemoteIpAddress)
                        _requestDataService.remoteAddress = context.Connection.RemoteIpAddress.ToString();
                }
                else
                {
                    _requestDataService.remoteAddress = headerX_Forwarded_For.ToString();
                }

                var headerX_Forwarded_Proto = context.Request.Headers["X-Forwarded-Proto"];
                if (string.IsNullOrWhiteSpace(headerX_Forwarded_Proto))
                {
                    if (false == string.IsNullOrWhiteSpace(context.Request.Scheme))
                        _requestDataService.scheme = context.Request.Scheme;
                }
                else
                {
                    _requestDataService.scheme = headerX_Forwarded_Proto.ToString();
                }

                var headerPort = context.Request.Headers["Port"];
                if (string.IsNullOrWhiteSpace(headerPort))
                {
                    _requestDataService.port = null == context.Request.Host.Port ? 0 : (int)context.Request.Host.Port;
                }
                else
                {
                    _requestDataService.port = int.Parse(headerPort.ToString());
                }

                //var headerX_Forwarded_Host = context.Request.Headers["X-Forwarded-Host"];
                //if (string.IsNullOrWhiteSpace(headerX_Forwarded_Host))
                //{
                //    _requestDataService.host = context.Request.Host.Host;
                //}
                //else
                //{
                //    _requestDataService.host = headerX_Forwarded_Host.ToString();
                //}

                //if (0 != _requestDataService.port && _requestDataService.port != 80 && _requestDataService.port != 443)
                //    _requestDataService.host = _requestDataService.host + ":" + _requestDataService.port;

                if (_requestDataService.port == 443 || _requestDataService.port == 80 || 0 == _requestDataService.port)
                {
                    _requestDataService.host = context.Request.Host.Host;
                }
                else
                {
                    _requestDataService.host = context.Request.Host.Host + ":" + _requestDataService.port;
                }

                var headerUserAgent = context.Request.Headers["User-Agent"];
                if (false == string.IsNullOrWhiteSpace(headerUserAgent))
                    _requestDataService.userAgent = headerUserAgent.ToString();

                if (0 < _firewallService.allowIPAddresses.Count)
                {
                    bool isAllowed = false;
                    foreach (var allowIP in _firewallService.allowIPAddresses)
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
                        if (_webHostEnvironment.IsDevelopment())
                            _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: not allowed ip.");

                        await _hubSend.DeniedAck(invocationContext.Context.ConnectionId, new PDeniedAck { error = Errors.Common_Denied });
                        return new object { };
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
                            if (_webHostEnvironment.IsDevelopment())
                                _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: denied ip.");

                            await _hubSend.DeniedAck(invocationContext.Context.ConnectionId, new PDeniedAck { error = Errors.Common_Denied });
                            return new object { };
                        }
                    }
                }

                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception calling '{invocationContext.HubMethodName}': {ex}");
                throw;
            }
        }

        // Optional method
        public async Task OnConnectedAsync(HubLifetimeContext hubLifeTimeContext, Func<HubLifetimeContext, Task> next)
        {
            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"Calling hub method 'OnConnectedAsync({hubLifeTimeContext.Context.ConnectionId})'");

            try
            {
                var context = hubLifeTimeContext.Context.GetHttpContext();

                if (null == context)
                    return;

                var headerServerAddress = context.Request.Headers["ServerAddress"];
                if (string.IsNullOrWhiteSpace(headerServerAddress))
                {
                    var localIpAddress = context.Connection.LocalIpAddress;
                    if (null != localIpAddress)
                        _requestDataService.serverAddress = localIpAddress.ToString();
                }
                else
                {
                    _requestDataService.serverAddress = headerServerAddress.ToString();
                }

                var headerX_Forwarded_For = context.Request.Headers["X-Forwarded-For"];
                if (string.IsNullOrWhiteSpace(headerX_Forwarded_For))
                {
                    if (null != context.Connection.RemoteIpAddress)
                        _requestDataService.remoteAddress = context.Connection.RemoteIpAddress.ToString();
                }
                else
                {
                    _requestDataService.remoteAddress = headerX_Forwarded_For.ToString();
                }

                var headerX_Forwarded_Proto = context.Request.Headers["X-Forwarded-Proto"];
                if (string.IsNullOrWhiteSpace(headerX_Forwarded_Proto))
                {
                    if (false == string.IsNullOrWhiteSpace(context.Request.Scheme))
                        _requestDataService.scheme = context.Request.Scheme;
                }
                else
                {
                    _requestDataService.scheme = headerX_Forwarded_Proto.ToString();
                }

                var headerPort = context.Request.Headers["Port"];
                if (string.IsNullOrWhiteSpace(headerPort))
                {
                    _requestDataService.port = null == context.Request.Host.Port ? 0 : (int)context.Request.Host.Port;
                }
                else
                {
                    _requestDataService.port = int.Parse(headerPort.ToString());
                }

                //var headerX_Forwarded_Host = context.Request.Headers["X-Forwarded-Host"];
                //if (string.IsNullOrWhiteSpace(headerX_Forwarded_Host))
                //{
                //    _requestDataService.host = context.Request.Host.Host;
                //}
                //else
                //{
                //    _requestDataService.host = headerX_Forwarded_Host.ToString();
                //}

                //if (0 != _requestDataService.port && _requestDataService.port != 80 && _requestDataService.port != 443)
                //    _requestDataService.host = _requestDataService.host + ":" + _requestDataService.port;

                if (_requestDataService.port == 443 || _requestDataService.port == 80 || 0 == _requestDataService.port)
                {
                    _requestDataService.host = context.Request.Host.Host;
                }
                else
                {
                    _requestDataService.host = context.Request.Host.Host + ":" + _requestDataService.port;
                }

                var headerUserAgent = context.Request.Headers["User-Agent"];
                if (false == string.IsNullOrWhiteSpace(headerUserAgent))
                    _requestDataService.userAgent = headerUserAgent.ToString();

                if (0 < _firewallService.allowIPAddresses.Count)
                {
                    bool isAllowed = false;
                    foreach (var allowIP in _firewallService.allowIPAddresses)
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
                        if (_webHostEnvironment.IsDevelopment())
                            _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: not allowed ip.");

                        await _hubSend.DeniedAck(hubLifeTimeContext.Context.ConnectionId, new PDeniedAck { error = Errors.Common_Denied });
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
                            if (_webHostEnvironment.IsDevelopment())
                                _logger.LogInformation($"[{(_requestDataService.remoteAddress)}]: denied ip.");

                            await _hubSend.DeniedAck(hubLifeTimeContext.Context.ConnectionId, new PDeniedAck { error = Errors.Common_Denied });
                            return;
                        }
                    }
                }

                await next(hubLifeTimeContext);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception calling 'OnConnected': {ex}");
                throw;
            }
        }

        // Optional method
        public Task OnDisconnectedAsync(HubLifetimeContext hubLifeTimeContext, Exception? exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"Calling hub method 'OnDisconnectedAsync({hubLifeTimeContext.Context.ConnectionId})'");

            return next(hubLifeTimeContext, exception ?? new Exception());
        }
    }
}
