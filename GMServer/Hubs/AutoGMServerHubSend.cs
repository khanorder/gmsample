using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagePack;
using Newtonsoft.Json;
using GMServer.Services;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Vars;
using NGEL.Data.Models;
using NGEL.Data.Helpers;
using NGEL.Data.Settings;

namespace GMServer.Hubs
{
    public class AutoGMServerHubSend
    {
        private readonly GMAuthentication _gmAuthentication;
        private readonly DBHelper _dbHelper;
        private readonly GMLogService _gmLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonSettings _commonSettings;
        private readonly ServerStateService _serverStateService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AutoGMServerHubSend> _logger;
        private readonly IHubContext<GMServerHub> _hub;

        public AutoGMServerHubSend(
            GMAuthentication gmAuthentication,
            DBHelper dbHelper,
            GMLogService gmLogService,
            IHttpContextAccessor httpContextAccessor,
            CommonSettings commonSettings,
            ServerStateService serverStateService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<AutoGMServerHubSend> logger,
            IHubContext<GMServerHub> hub
        )
        {
            _gmAuthentication = gmAuthentication;
            _dbHelper = dbHelper;
            _gmLogService = gmLogService;
            _httpContextAccessor = httpContextAccessor;
            _commonSettings = commonSettings;
            _serverStateService = serverStateService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _hub = hub;
        }

        public async Task ConnectedAck(string connectionID, PConnectedAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"ConnectedAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("ConnectedAck", message);
        }

        public async Task CheckAuthenticationAck(string connectionID, PCheckAuthenticationAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"CheckAuthenticationAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"CheckAuthenticationAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {{ token: \"{packet.token}\" }}");

            await _hub.Clients.Client(connectionID).SendAsync("CheckAuthenticationAck", message);
        }

        public async Task CheckConnectionAck(string connectionID, PCheckConnectionAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"CheckConnectionAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"CheckConnectionAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("CheckConnectionAck", message);
        }

        public async Task CommonNoticeAck(string connectionID, PCommonNoticeAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"CommonNoticeAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"CommonNoticeAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("CommonNoticeAck", message);
        }

        public async Task SignInAck(string connectionID, PSignInAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }


            await _hub.Clients.Client(connectionID).SendAsync("SignInAck", message);
        }

        public async Task SignInLDAPAck(string connectionID, PSignInLDAPAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }


            await _hub.Clients.Client(connectionID).SendAsync("SignInLDAPAck", message);
        }

        public async Task SignInEmailAck(string connectionID, PSignInEmailAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }


            await _hub.Clients.Client(connectionID).SendAsync("SignInEmailAck", message);
        }

        public async Task DataTableAck(string connectionID, PDataTableAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }


            await _hub.Clients.Client(connectionID).SendAsync("DataTableAck", message);
        }

        public async Task SendGameMailResultAck(string connectionID, PSendGameMailResultAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SendGameMailResultAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("SendGameMailResultAck", message);
        }

        public async Task InsertEventMailResultAck(string connectionID, PInsertEventMailResultAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"InsertEventMailResultAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("InsertEventMailResultAck", message);
        }

        public async Task UpdateEventMailResultAck(string connectionID, PUpdateEventMailResultAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"UpdateEventMailResultAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("UpdateEventMailResultAck", message);
        }

        public async Task RemoveEventMailResultAck(string connectionID, PRemoveEventMailResultAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"RemoveEventMailResultAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("RemoveEventMailResultAck", message);
        }

        public async Task SendUserJobAck(string connectionID, PSendUserJobAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SendUserJobAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("SendUserJobAck", message);
        }

        public async Task SignOutAck(string connectionID, PSignOutAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SignOutAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("SignOutAck", message);
        }

        public async Task DeniedAck(string connectionID, PDeniedAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"DeniedAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("DeniedAck", message);
        }

        public async Task SendChattingMessageResultAck(string connectionID, PSendChattingMessageResultAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"SendChattingMessageResultAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("SendChattingMessageResultAck", message);
        }

        public async Task ReceiveChattingMessageAck(string connectionID, PReceiveChattingMessageAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"ReceiveChattingMessageAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("ReceiveChattingMessageAck", message);
        }

        public async Task TestAck(string connectionID, PTestAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - connectionID: {connectionID}, error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"TestAck - connectionID: {connectionID}, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.Client(connectionID).SendAsync("TestAck", message);
        }

        public async Task NoticeAck(PNoticeAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"NoticeAck - All, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.All.SendAsync("NoticeAck", message);
        }

        public async Task ReceiveChattingMessageAllAck(PReceiveChattingMessageAllAck packet)
        {
            var error = packet.SerializeMessagePack(out var message);
            if (Errors.None != error)
            {
                _logger.LogError($"ConnectedAck - error: {error.ToString()}");
                return;
            }

            if (_webHostEnvironment.IsDevelopment())
                _logger.LogInformation($"ReceiveChattingMessageAllAck - All, error: {packet.error.ToString()}, packet: {JsonConvert.SerializeObject(packet)}");

            await _hub.Clients.All.SendAsync("ReceiveChattingMessageAllAck", message);
        }

    }
}
