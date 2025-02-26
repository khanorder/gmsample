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
using Microsoft.AspNetCore.Http;
using NGEL.Data;
using NGEL.Data.Helpers;
using System.IO;

namespace GMServer.Filters
{
    public class GMAuthorizationFilter: IAuthorizationFilter
    {
        private readonly ILogger<GMAuthorizationFilter> _logger;
        private readonly DBHelper _dbHelper;
        private readonly GMAuthentication _gmAuthentication;
        private readonly CommonSettings _commonSettings;
        private readonly IWebHostEnvironment _environment;
        public GMAuthorizationFilter(ILogger<GMAuthorizationFilter> logger, DBHelper dbHelper, GMAuthentication gmAuthentication, CommonSettings commonSettings, IWebHostEnvironment environment)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _gmAuthentication = gmAuthentication;
            _commonSettings = commonSettings;
            _environment = environment;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_environment.IsDevelopment())
                _logger.LogDebug($"[BEFORE] authFilter", ConsoleColor.Green);

            if (_environment.IsDevelopment())
                _logger.LogDebug($"[AFTER] authFilter.", ConsoleColor.Green);
        }
    }
}
