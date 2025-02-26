using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging.Console;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NGEL.Data;
using NGEL.Data.DB;
using NGEL.Data.Settings;
using NGEL.Data.Models;
using NGEL.Data.Tables;
using NGEL.Data.Helpers;
using NGEL.Data.Tables.Models;
using NGEL.Data.HttpFormatter;
using GMServer.Services.Extensions;
using GMServer.Services;
using GMServer.Filters;
using GMServer.ScheduledJobs;
using GMServer.Hubs;
using System.Text.RegularExpressions;
using NPOI.SS.Formula.Functions;

var builder = WebApplication.CreateBuilder(args);
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSimpleConsole(options =>
    {
        options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
        options.ColorBehavior = LoggerColorBehavior.Disabled;
    });
});
var logger = loggerFactory.CreateLogger<Program>();

var logLevel = LogLevel.Information;
if (builder.Environment.IsProduction())
    logLevel = LogLevel.Warning;

var logFilePath = Path.Combine(builder.Environment.ContentRootPath, "CacheFiles", "Logs", "Log.txt");
var logFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext} at {UserAgent}) {Message}{NewLine}{Exception}";

builder.Logging.AddFile(logFilePath, logLevel, null, false, 1073741824L, 31, logFormat);
builder.Logging.AddConsole(options =>
{
    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
});

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("commonsettings.Development.json", optional: false, reloadOnChange: false);
    builder.Configuration.AddJsonFile("navmenusettings.Development.json", optional: false, reloadOnChange: false);
    builder.Configuration.AddJsonFile("jwtsettings.Development.json", optional: false, reloadOnChange: false);
}
else
{
    builder.Configuration.AddJsonFile("commonsettings.json", optional: false, reloadOnChange: false);
    builder.Configuration.AddJsonFile("navmenusettings.json", optional: false, reloadOnChange: false);
    builder.Configuration.AddJsonFile("jwtsettings.json", optional: false, reloadOnChange: false);
}

var commonSettings = builder.Configuration.GetSection("CommonSettings").Get<CommonSettings>();
if (null == commonSettings)
{
    logger.LogError("commonSetting is null.");
    return;
}

if (null == commonSettings.databases || 1 > commonSettings.databases.Count)
{
    logger.LogError("databases connection informations are null.");
    return;
}

var gameManagerDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.GameManager);
if (null == gameManagerDBConnection || string.IsNullOrWhiteSpace(gameManagerDBConnection.connectionString))
{
    logger.LogError("gamemanager database connection information is null.");
    return;
}

var gameManagerWriteOnlyDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.GameManagerWriteOnly);
if (null == gameManagerWriteOnlyDBConnection || string.IsNullOrWhiteSpace(gameManagerWriteOnlyDBConnection.connectionString))
{
    logger.LogError("gamemanager write only database connection information is null.");
    return;
}

var gmLogDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.GMLog);
if (null == gmLogDBConnection || string.IsNullOrWhiteSpace(gmLogDBConnection.connectionString))
{
    logger.LogError("gamemanager log database connection information is null.");
    return;
}

var gmLogWriteOnlyDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.GMLogWriteOnly);
if (null == gmLogWriteOnlyDBConnection || string.IsNullOrWhiteSpace(gmLogWriteOnlyDBConnection.connectionString))
{
    logger.LogError("gamemanager log write only database connection information is null.");
    return;
}

var adminDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.Admin);
if (null == adminDBConnection || string.IsNullOrWhiteSpace(adminDBConnection.connectionString))
{
    logger.LogError("admin database connection information is null.");
    return;
}

var adminWriteOnlyDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.AdminWriteOnly);
if (null == adminWriteOnlyDBConnection || string.IsNullOrWhiteSpace(adminWriteOnlyDBConnection.connectionString))
{
    logger.LogError("admin write only database connection information is null.");
    return;
}

var visitWriteOnlyDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.VisitWriteOnly);
if (null == visitWriteOnlyDBConnection || string.IsNullOrWhiteSpace(visitWriteOnlyDBConnection.connectionString))
{
    logger.LogError("visit write only database connection information is null.");
    return;
}

var gameDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.Game);
if (null == gameDBConnection || string.IsNullOrWhiteSpace(gameDBConnection.connectionString))
{
    logger.LogError("game database connection information is null.");
    return;
}

var gameWriteOnlyDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.GameWriteOnly);
if (null == gameWriteOnlyDBConnection || string.IsNullOrWhiteSpace(gameWriteOnlyDBConnection.connectionString))
{
    logger.LogError("game write only database connection information is null.");
    return;
}

var logDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.Log);
if (null == logDBConnection || string.IsNullOrWhiteSpace(logDBConnection.connectionString))
{
    logger.LogError("log database connection information is null.");
    return;
}

var chatLogDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.ChatLog);
if (null == chatLogDBConnection || string.IsNullOrWhiteSpace(chatLogDBConnection.connectionString))
{
    logger.LogError("chat Log database connection information is null.");
    return;
}

var activeUserDBConnection = commonSettings.databases.Find(_ => _.dbRole == Defines.DatabaseRole.ActiveUser);
if (null == activeUserDBConnection || string.IsNullOrWhiteSpace(activeUserDBConnection.connectionString))
{
    logger.LogError("activeUser database connection information is null.");
    return;
}

var gameManagerDBConnectionString = gameManagerDBConnection.connectionString;
var gameManagerWriteOnlyDBConnectionString = gameManagerWriteOnlyDBConnection.connectionString;
var gmLogDBConnectionString = gmLogDBConnection.connectionString;
var gmLogWriteOnlyDBConnectionString = gmLogWriteOnlyDBConnection.connectionString;
var adminDBConnectionString = adminDBConnection.connectionString;
var adminWriteOnlyDBConnectionString = adminWriteOnlyDBConnection.connectionString;
var visitWriteOnlyDBConnectionString = visitWriteOnlyDBConnection.connectionString;
var gameDBConnectionString = gameDBConnection.connectionString;
var gameWriteOnlyDBConnectionString = gameWriteOnlyDBConnection.connectionString;
var logDBConnectionString = logDBConnection.connectionString;
var chatLogDBConnectionString = chatLogDBConnection.connectionString;
var activeUserDBConnectionString = activeUserDBConnection.connectionString;
if (1 > commonSettings.clientInfo.Count)
{
    logger.LogError("commonSettings.clientInfo is empty.");
    return;
}

if (string.IsNullOrWhiteSpace(commonSettings.uploadFolderName))
    commonSettings.uploadFolderName = "files";

var navMenuSettings = builder.Configuration.GetSection("NavMenuSettings").Get<NavMenuSettings>();
if (null == navMenuSettings)
{
    logger.LogError("navMenuSettings is null.");
    return;
}

if (null != navMenuSettings)
{
    var mainMenus = new NavMenu
    {
        name = "메인 메뉴",
        path = "/",
        parameters = new Dictionary<string, string>(),
        policyName = "",
        roles = new string[] { },
        children = new List<NavMenu>()
    };

    var superMenus = new NavMenu
    {
        name = "관리자 메뉴",
        path = "/Manages",
        parameters = new Dictionary<string, string>(),
        policyName = "Administrator",
        roles = new string[] { "최고 권한" },
        children = new List<NavMenu>()
        {
            new NavMenu
            {
                name = "계정 관리",
                path = "/Manages/Users",
                parameters = new Dictionary<string, string>(),
                policyName = "Administrator",
                roles = new string[] { "최고 권한" },
                children = new List<NavMenu>(),
            }
        }
    };

    if (null == navMenuSettings.navMenus)
        navMenuSettings.navMenus = new List<NavMenu>();

    navMenuSettings.navMenus.Insert(0, mainMenus);
    navMenuSettings.navMenus.Add(superMenus);
}

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (null == navMenuSettings)
{
    logger.LogError("jwtSettings is null.");
    return;
}

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("최고 권한"));
    if (null != navMenuSettings && 0 < navMenuSettings.navMenus.Count)
        MenuRoleToPolicy(options, navMenuSettings.navMenus);
});

builder.Services.AddSignalR(options =>
    {
        options.MaximumReceiveMessageSize = null;
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(300);
        options.KeepAliveInterval = TimeSpan.FromSeconds(150);
        //options.AddFilter<GMServerHubFilter>();
    })
    .AddHubOptions<GMServerHub>(options =>
    {
        options.MaximumReceiveMessageSize = null;
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(300);
        options.KeepAliveInterval = TimeSpan.FromSeconds(150);
        options.AddFilter<GMServerHubFilter>();
    });

// Add services to the container.
builder.Services
    .AddControllers(config =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        config.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    })
    .AddMvcOptions(options =>
    {
        options.InputFormatters.Add(new TextPlainInputFormatter());
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    if (0 < commonSettings.clientInfo.Count)
    {
        foreach (var client in commonSettings.clientInfo)
            options.AddServer(new OpenApiServer() { Url = client.host });
    }
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(10);
});
builder.Services.Configure<CookieTempDataProviderOptions>(options => options.Cookie.Name = commonSettings.cookieName + ".Data");

builder.Services.AddJWTTokenServices(builder.Configuration, builder.Environment);
builder.Services.AddDataTableServices();

builder.Services.AddSingleton<ServerStateService>();
builder.Services.AddSingleton<MaintenanceService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<DBHelperGameManager>();
builder.Services.AddSingleton<DBHelperGameManagerWriteOnly>();
builder.Services.AddSingleton<DBHelperGMLog>();
builder.Services.AddSingleton<DBHelperGMLogWriteOnly>();
builder.Services.AddSingleton<DBHelperAdmin>();
builder.Services.AddSingleton<DBHelperAdminWriteOnly>();
builder.Services.AddSingleton<DBHelperVisitWriteOnly>();
builder.Services.AddSingleton<DBHelperGame>();
builder.Services.AddSingleton<DBHelperGameWriteOnly>();
builder.Services.AddSingleton<DBHelperLog>();
builder.Services.AddSingleton<DBHelperChatLog>();
builder.Services.AddSingleton<DBHelperActiveUser>();
builder.Services.AddSingleton<DBHelper>();
builder.Services.AddSingleton<GMLogService>();
builder.Services.AddSingleton<GMAuthentication>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<MicrosoftSigninProvider>();
builder.Services.AddSingleton<KakaoSigninProvider>();
builder.Services.AddSingleton<NaverSigninProvider>();
builder.Services.AddSingleton(commonSettings);
builder.Services.AddSingleton(navMenuSettings);
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<StompService>();
builder.Services.AddSingleton<ExcelConvert>();
builder.Services.AddSingleton<LobbyTimeService>();
builder.Services.AddSingleton<AutoGMServerHubSend>();
builder.Services.AddSingleton<FirewallService>();
builder.Services.AddSingleton<CryptoService>();
builder.Services.AddSingleton<GMUserService>();

builder.Services.AddScoped<LineNotifyProvider>();
builder.Services.AddScoped<GMAsyncActionFilter>();
builder.Services.AddScoped<RequestDataService>();

#region Scheduled Job
//builder.Services.AddQuartz(q =>
//{
//    q.UseMicrosoftDependencyInjectionJobFactory();
//    var checkAuthJobKey = new JobKey("CheckAuthJob");

//    q.AddJob<CheckAuthJob>(opts => opts.WithIdentity(checkAuthJobKey));

//    q.AddTrigger(opts =>
//    {
//        opts.ForJob(checkAuthJobKey).WithIdentity("CheckAuthJob-trigger").WithCronSchedule("*/5 * * * * ?");
//    });
//});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var checkServerStateJobKey = new JobKey("CheckServerState");

    q.AddJob<CheckServerStateJob>(opts => opts.WithIdentity(checkServerStateJobKey));

    q.AddTrigger(opts =>
    {
        opts.ForJob(checkServerStateJobKey).WithIdentity("CheckServerStateJob-trigger").WithCronSchedule("* * * * * ?");
    });
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var checkChattingNoticeJobKey = new JobKey("CheckChattingNotice");

    q.AddJob<CheckChattingNoticeJob>(opts => opts.WithIdentity(checkChattingNoticeJobKey));

    q.AddTrigger(opts =>
    {
        opts.ForJob(checkChattingNoticeJobKey).WithIdentity("CheckChattingNoticeJob-trigger").WithCronSchedule("0 * * * * ?");
    });
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var checkMaintenenceJobKey = new JobKey("CheckMaintenence");

    q.AddJob<CheckMaintenenceJob>(opts => opts.WithIdentity(checkMaintenenceJobKey));

    q.AddTrigger(opts =>
    {
        opts.ForJob(checkMaintenenceJobKey).WithIdentity("CheckMaintenenceJob-trigger").WithCronSchedule("0 * * * * ?");
    });
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var checkFirewallJobKey = new JobKey("CheckFirewall");

    q.AddJob<CheckFirewallJob>(opts => opts.WithIdentity(checkFirewallJobKey));

    q.AddTrigger(opts =>
    {
        opts.ForJob(checkFirewallJobKey).WithIdentity("CheckFirewallJob-trigger").WithCronSchedule("0 * * * * ?");
    });
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var checkExpireLogJobKey = new JobKey("CheckExpireLog");

    q.AddJob<CheckExpireLogJob>(opts => opts.WithIdentity(checkExpireLogJobKey));

    q.AddTrigger(opts =>
    {
        opts.ForJob(checkExpireLogJobKey).WithIdentity("CheckExpireLog-trigger").WithCronSchedule("0 0 0 * * ?");
    });
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    if (Defines.ServerStateType.InsideTest != commonSettings.serverState)
        app.UseHttpsRedirection();
}

using (var serviceScope = app.Services.CreateAsyncScope())
{
    #region DBHelper Init
    var dbHelperGameManager = serviceScope.ServiceProvider.GetRequiredService<DBHelperGameManager>();
    dbHelperGameManager.InitDBConnect(gameManagerDBConnectionString);

    var dbHelperGameManagerWriteOnly = serviceScope.ServiceProvider.GetRequiredService<DBHelperGameManagerWriteOnly>();
    dbHelperGameManagerWriteOnly.InitDBConnect(gameManagerWriteOnlyDBConnectionString);

    if (null != navMenuSettings && null != navMenuSettings.navMenus && 0 < navMenuSettings.navMenus.Count)
        await InsertRole(navMenuSettings.navMenus, dbHelperGameManager, dbHelperGameManagerWriteOnly);

    var testerName = "테스터";
    var selectTestUserTask = await dbHelperGameManager.SelectUserByName(testerName);
    if (false == selectTestUserTask.Item1 || null == selectTestUserTask.Item2)
    {
        var testerEmail = "test@gm.baejangho.com";
        var selectTestUserEmailTask = await dbHelperGameManager.SelectUserEmailByEmail(testerEmail);
        if (true == selectTestUserEmailTask.Item1 && null != selectTestUserEmailTask.Item2)
            await dbHelperGameManagerWriteOnly.DeleteUserEmail(selectTestUserEmailTask.Item2.id);

        var gmAuthentication = serviceScope.ServiceProvider.GetRequiredService<GMAuthentication>();
        await gmAuthentication.SignUpByOAuth(Defines.OAuthProvider.CustomEmail, Guid.NewGuid().ToString(), testerEmail, testerName);
    }

    var testerAdminName = "테스트 관리자";
    var selectTestAdminUserTask = await dbHelperGameManager.SelectUserByName(testerAdminName);
    if (false == selectTestAdminUserTask.Item1 || null == selectTestAdminUserTask.Item2)
    {
        var testerAdminEmail = "admin@gm.baejangho.com";
        var selectTestAdminUserEmailTask = await dbHelperGameManager.SelectUserEmailByEmail(testerAdminEmail);
        if (true == selectTestAdminUserEmailTask.Item1 && null != selectTestAdminUserEmailTask.Item2)
            await dbHelperGameManagerWriteOnly.DeleteUserEmail(selectTestAdminUserEmailTask.Item2.id);

        var gmAuthentication = serviceScope.ServiceProvider.GetRequiredService<GMAuthentication>();
        await gmAuthentication.SignUpByOAuth(Defines.OAuthProvider.CustomEmail, Guid.NewGuid().ToString(), testerAdminEmail, testerAdminName);
    }

    var dbHelperGMLog = serviceScope.ServiceProvider.GetRequiredService<DBHelperGMLog>();
    dbHelperGMLog.InitDBConnect(gmLogDBConnectionString);

    var dbHelperGMLogWriteOnly = serviceScope.ServiceProvider.GetRequiredService<DBHelperGMLogWriteOnly>();
    dbHelperGMLogWriteOnly.InitDBConnect(gmLogWriteOnlyDBConnectionString);

    var dbHelperAdmin = serviceScope.ServiceProvider.GetRequiredService<DBHelperAdmin>();
    dbHelperAdmin.InitDBConnect(adminDBConnectionString);

    var dbHelperAdminWriteOnly = serviceScope.ServiceProvider.GetRequiredService<DBHelperAdminWriteOnly>();
    dbHelperAdminWriteOnly.InitDBConnect(adminWriteOnlyDBConnectionString);

    var dbHelperVisitWriteOnly = serviceScope.ServiceProvider.GetRequiredService<DBHelperVisitWriteOnly>();
    dbHelperVisitWriteOnly.InitDBConnect(visitWriteOnlyDBConnectionString);

    var dbHelperGame = serviceScope.ServiceProvider.GetRequiredService<DBHelperGame>();
    dbHelperGame.InitDBConnect(gameDBConnectionString);

    var dbHelperGameWriteOnly = serviceScope.ServiceProvider.GetRequiredService<DBHelperGameWriteOnly>();
    dbHelperGameWriteOnly.InitDBConnect(gameWriteOnlyDBConnectionString);

    var dbHelperLog = serviceScope.ServiceProvider.GetRequiredService<DBHelperLog>();
    dbHelperLog.InitDBConnect(logDBConnectionString);

    var dbHelperChatLog = serviceScope.ServiceProvider.GetRequiredService<DBHelperChatLog>();
    dbHelperChatLog.InitDBConnect(chatLogDBConnectionString);

    var dbHelperActiveUser = serviceScope.ServiceProvider.GetRequiredService<DBHelperActiveUser>();
    dbHelperActiveUser.InitDBConnect(activeUserDBConnectionString);
    #endregion

    var gmLogService = serviceScope.ServiceProvider.GetRequiredService<GMLogService>();
    await gmLogService.Init();

    var stompService = serviceScope.ServiceProvider.GetRequiredService<StompService>();
    await stompService.Connect();

    var maintenanceService = serviceScope.ServiceProvider.GetRequiredService<MaintenanceService>();
    await maintenanceService.Init();

    #region Load CSV Data Table
    var serverStateService = serviceScope.ServiceProvider.GetRequiredService<ServerStateService>();
    var stringDataTableService = serviceScope.ServiceProvider.GetRequiredService<StringDataTableService>();
    stringDataTableService.LoadTable();

    var achievementDataTableService = serviceScope.ServiceProvider.GetRequiredService<AchievementDataTableService>();
    achievementDataTableService.LoadTable();

    foreach (var achievementData in achievementDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == achievementData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            achievementData.NameString = stringData.SourceString;
            achievementData.NameStringWithID = $"{stringData.SourceString}({achievementData.ID})";
        }
        else
        {
            achievementData.NameString = achievementData.Name;
            achievementData.NameStringWithID = $"{achievementData.Name}({achievementData.ID})";
        }
    }

    var achievementGroupDataTableService = serviceScope.ServiceProvider.GetRequiredService<AchievementGroupDataTableService>();
    achievementGroupDataTableService.LoadTable();

    foreach (var achievementGroupData in achievementGroupDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == achievementGroupData.CategoryName);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            achievementGroupData.CategoryNameString = stringData.SourceString;
            achievementGroupData.CategoryNameStringWithID = $"{stringData.SourceString}({achievementGroupData.ID})";
        }
        else
        {
            achievementGroupData.CategoryNameString = achievementGroupData.CategoryName;
            achievementGroupData.CategoryNameStringWithID = $"{achievementGroupData.CategoryName}({achievementGroupData.ID})";
        }
    }

    var accountLevelTableService = serviceScope.ServiceProvider.GetRequiredService<AccountLevelTableService>();
    accountLevelTableService.LoadTable();

    var artifactDataTableService = serviceScope.ServiceProvider.GetRequiredService<ArtifactDataTableService>();
    artifactDataTableService.LoadTable();

    foreach (var artifactData in artifactDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == artifactData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            artifactData.NameString = stringData.SourceString;
            artifactData.NameStringWithID = $"{stringData.SourceString}({artifactData.ID})";
        }
        else
        {
            artifactData.NameString = artifactData.Name;
            artifactData.NameStringWithID = $"{artifactData.Name}({artifactData.ID})";
        }
    }

    var assetDataTableService = serviceScope.ServiceProvider.GetRequiredService<AssetDataTableService>();
    assetDataTableService.LoadTable();

    foreach (var asset in assetDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == asset.AssetString);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            asset.NameString = stringData.SourceString;
            asset.NameStringWithID = $"{stringData.SourceString}({asset.ID})";
        }
        else
        {
            asset.NameString = asset.AssetString;
            asset.NameStringWithID = $"{asset.AssetString}({asset.ID})";
        }
    }

    var attendanceDataTableService = serviceScope.ServiceProvider.GetRequiredService<AttendanceDataTableService>();
    attendanceDataTableService.LoadTable();

    foreach (var attendance in attendanceDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == attendance.Description);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            attendance.DescriptionString = stringData.SourceString;
            attendance.DescriptionStringWithID = $"{stringData.SourceString}({attendance.ID})";
        }
        else
        {
            attendance.DescriptionString = attendance.Description;
            attendance.DescriptionStringWithID = $"{attendance.Description}({attendance.ID})";
        }
    }

    var attendanceRewardDataTableService = serviceScope.ServiceProvider.GetRequiredService<AttendanceRewardDataTableService>();
    attendanceRewardDataTableService.LoadTable();

    var attributeDataTableService = serviceScope.ServiceProvider.GetRequiredService<AttributeDataTableService>();
    attributeDataTableService.LoadTable();

    foreach (var attribute in attributeDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == attribute.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            attribute.NameString = stringData.SourceString;
            attribute.NameStringWithID = $"{stringData.SourceString}({attribute.ID})";
        }
        else
        {
            attribute.NameString = attribute.Name;
            attribute.NameStringWithID = $"{attribute.Name}({attribute.ID})";
        }
    }

    var battleStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<BattleStoreDataTableService>();
    battleStoreDataTableService.LoadTable();

    var chapterDataTableService = serviceScope.ServiceProvider.GetRequiredService<ChapterDataTableService>();
    chapterDataTableService.LoadTable();

    foreach (var chapterData in chapterDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == chapterData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            chapterData.NameString = stringData.SourceString;
            chapterData.NameStringWithID = $"{stringData.SourceString}({chapterData.ID})";
        }
        else
        {
            chapterData.NameString = chapterData.Name;
            chapterData.NameStringWithID = $"{chapterData.Name}({chapterData.ID})";
        }
    }

    var characterDataTableService = serviceScope.ServiceProvider.GetRequiredService<CharacterDataTableService>();
    characterDataTableService.LoadTable();

    foreach (var characterData in characterDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == characterData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            characterData.NameString = stringData.SourceString;
            characterData.NameStringWithID = $"{stringData.SourceString}({characterData.ID})";
        }
        else
        {
            characterData.NameString = characterData.Name;
            characterData.NameStringWithID = $"{characterData.Name}({characterData.ID})";
        }
    }

    var collectionDataTableService = serviceScope.ServiceProvider.GetRequiredService<CollectionDataTableService>();
    collectionDataTableService.LoadTable();

    foreach (var collection in collectionDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == collection.GroupName);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            collection.GroupNameString = stringData.SourceString;
            collection.GroupNameStringWithID = $"{stringData.SourceString}({collection.ID})";
        }
        else
        {
            collection.GroupNameString = collection.GroupName;
            collection.GroupNameStringWithID = $"{collection.GroupName}({collection.ID})";
        }
    }

    var collectionGroupDataTableService = serviceScope.ServiceProvider.GetRequiredService<CollectionGroupDataTableService>();
    collectionGroupDataTableService.LoadTable();

    foreach (var collectionGroup in collectionGroupDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == collectionGroup.CategoryName);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            collectionGroup.CategoryNameString = stringData.SourceString;
            collectionGroup.CategoryNameStringWithID = $"{stringData.SourceString}({collectionGroup.ID})";
        }
        else
        {
            collectionGroup.CategoryNameString = collectionGroup.CategoryName;
            collectionGroup.CategoryNameStringWithID = $"{collectionGroup.CategoryName}({collectionGroup.ID})";
        }
    }

    var colorDataTableService = serviceScope.ServiceProvider.GetRequiredService<ColorDataTableService>();
    colorDataTableService.LoadTable();

    var dataChipStoreListDataTableService = serviceScope.ServiceProvider.GetRequiredService<DataChipStoreListDataTableService>();
    dataChipStoreListDataTableService.LoadTable();

    var entitlementDataTableService = serviceScope.ServiceProvider.GetRequiredService<EntitlementDataTableService>();
    entitlementDataTableService.LoadTable();

    foreach (var entitlement in entitlementDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == entitlement.EntitlementName);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            entitlement.NameString = stringData.SourceString;
            entitlement.NameStringWithID = $"{stringData.SourceString}({entitlement.ID})";
        }
        else
        {
            entitlement.NameString = entitlement.EntitlementName;
            entitlement.NameStringWithID = $"{entitlement.EntitlementName}({entitlement.ID})";
        }
    }

    var eventStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<EventStoreDataTableService>();
    eventStoreDataTableService.LoadTable();

    var expressionDataTableService = serviceScope.ServiceProvider.GetRequiredService<ExpressionDataTableService>();
    expressionDataTableService.LoadTable();

    foreach (var expressionData in expressionDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == expressionData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            expressionData.NameString = stringData.SourceString;
            expressionData.NameStringWithID = $"{stringData.SourceString}({expressionData.ID})";
        }
        else
        {
            expressionData.NameString = expressionData.Name;
            expressionData.NameStringWithID = $"{expressionData.Name}({expressionData.ID})";
        }
    }

    var globalDefineDataTableService = serviceScope.ServiceProvider.GetRequiredService<GlobalDefineDataTableService>();
    globalDefineDataTableService.LoadTable();

    var guideMissionDataTableService = serviceScope.ServiceProvider.GetRequiredService<GuideMissionDataTableService>();
    guideMissionDataTableService.LoadTable();

    foreach (var guideMission in guideMissionDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == guideMission.Title);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            guideMission.TitleString = stringData.SourceString;
            guideMission.TitleStringWithID = $"{stringData.SourceString}({guideMission.ID})";
        }
        else
        {
            guideMission.TitleString = guideMission.Title;
            guideMission.TitleStringWithID = $"{guideMission.Title}({guideMission.ID})";
        }
    }

    var guideMissionStepRewardTableService = serviceScope.ServiceProvider.GetRequiredService<GuideMissionStepRewardTableService>();
    guideMissionStepRewardTableService.LoadTable();

    var glitchStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<GlitchStoreDataTableService>();
    glitchStoreDataTableService.LoadTable();

    var goldMedalStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<GoldMedalStoreDataTableService>();
    goldMedalStoreDataTableService.LoadTable();

    var incubationDataTableService = serviceScope.ServiceProvider.GetRequiredService<IncubationDataTableService>();
    incubationDataTableService.LoadTable();

    foreach (var incubation in incubationDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == incubation.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            incubation.NameString = stringData.SourceString;
            incubation.NameStringWithID = $"{stringData.SourceString}({incubation.ID})";
        }
        else
        {
            incubation.NameString = incubation.Name;
            incubation.NameStringWithID = $"{incubation.Name}({incubation.ID})";
        }
    }

    var instantGuideUIDataTableService = serviceScope.ServiceProvider.GetRequiredService<InstantGuideUIDataTableService>();
    instantGuideUIDataTableService.LoadTable();

    foreach (var instantGuideUI in instantGuideUIDataTableService.datas)
    {
        var categoryNameStringData = stringDataTableService.datas.Find(_ => _.Key == instantGuideUI.GuideCategoryName);
        if (null != categoryNameStringData && false == string.IsNullOrWhiteSpace(categoryNameStringData.SourceString))
        {
            instantGuideUI.GuideCategoryNameString = categoryNameStringData.SourceString;
            instantGuideUI.GuideCategoryNameStringWithID = $"{categoryNameStringData.SourceString}({instantGuideUI.ID})";
        }
        else
        {
            instantGuideUI.GuideCategoryNameString = instantGuideUI.GuideCategoryName;
            instantGuideUI.GuideCategoryNameStringWithID = $"{instantGuideUI.GuideCategoryName}({instantGuideUI.ID})";
        }

        var titleStringData = stringDataTableService.datas.Find(_ => _.Key == instantGuideUI.Title);
        if (null != titleStringData && false == string.IsNullOrWhiteSpace(titleStringData.SourceString))
        {
            instantGuideUI.TitleString = titleStringData.SourceString;
            instantGuideUI.TitleStringWithID = $"{titleStringData.SourceString}({instantGuideUI.ID})";
        }
        else
        {
            instantGuideUI.TitleString = instantGuideUI.Title;
            instantGuideUI.TitleStringWithID = $"{instantGuideUI.Title}({instantGuideUI.ID})";
        }
    }

    var itemTableService = serviceScope.ServiceProvider.GetRequiredService<ItemTableService>();
    itemTableService.LoadTable();

    foreach (var item in itemTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == item.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            item.NameString = stringData.SourceString;
            item.NameStringWithID = $"{stringData.SourceString}({item.ID})";
        }
        else
        {
            item.NameString = item.Name;
            item.NameStringWithID = $"{item.Name}({item.ID})";
        }
    }

    var introduceDataTableService = serviceScope.ServiceProvider.GetRequiredService<IntroduceDataTableService>();
    introduceDataTableService.LoadTable();

    foreach (var introduceData in introduceDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == introduceData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            introduceData.NameString = stringData.SourceString;
            introduceData.NameStringWithID = $"{stringData.SourceString}({introduceData.ID})";
        }
        else
        {
            introduceData.NameString = introduceData.Name;
            introduceData.NameStringWithID = $"{introduceData.Name}({introduceData.ID})";
        }
    }

    var levelUpBuffListDataTableService = serviceScope.ServiceProvider.GetRequiredService<LevelUpBuffListDataTableService>();
    levelUpBuffListDataTableService.LoadTable();

    foreach (var item in levelUpBuffListDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == item.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            item.NameString = stringData.SourceString;
            item.NameStringWithID = $"{stringData.SourceString}({item.ID})";
        }
        else
        {
            item.NameString = item.Name;
            item.NameStringWithID = $"{item.Name}({item.ID})";
        }
    }

    var missionDataTableService = serviceScope.ServiceProvider.GetRequiredService<MissionDataTableService>();
    missionDataTableService.LoadTable();

    var parameterDataTableService = serviceScope.ServiceProvider.GetRequiredService<ParameterDataTableService>();
    parameterDataTableService.LoadTable();

    foreach (var parameterData in parameterDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == parameterData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            parameterData.NameString = stringData.SourceString;
            parameterData.NameStringWithParameter = $"{stringData.SourceString}({parameterData.ParameterName})";
        }
        else
        {
            parameterData.NameString = parameterData.NameString;
            parameterData.NameStringWithParameter = $"{parameterData.Name}({parameterData.ParameterName})";
        }
    }

    var penaltyDataTableService = serviceScope.ServiceProvider.GetRequiredService<PenaltyDataTableService>();
    penaltyDataTableService.LoadTable();

    foreach (var penaltyData in penaltyDataTableService.datas)
    {
        var reportStringData = stringDataTableService.datas.Find(_ => _.Key == penaltyData.ReportStateText);
        if (null != reportStringData && false == string.IsNullOrWhiteSpace(reportStringData.SourceString))
        {
            penaltyData.ReportStateTextString = reportStringData.SourceString;
            penaltyData.ReportStateTextStringWithID = $"{reportStringData.SourceString}({penaltyData.ReportState})";
        }
        else
        {
            penaltyData.ReportStateTextString = penaltyData.ReportStateText;
            penaltyData.ReportStateTextStringWithID = $"{penaltyData.ReportStateText}({penaltyData.ReportState})";
        }

        var typeStringData = stringDataTableService.datas.Find(_ => _.Key == penaltyData.PenaltyTypeText);
        if (null != typeStringData && false == string.IsNullOrWhiteSpace(typeStringData.SourceString))
        {
            penaltyData.PenaltyTypeTextString = typeStringData.SourceString;
            penaltyData.PenaltyTypeTextStringWithID = $"{typeStringData.SourceString}({penaltyData.PenaltyType})";
        }
        else
        {
            penaltyData.PenaltyTypeTextString = penaltyData.PenaltyTypeText;
            penaltyData.PenaltyTypeTextStringWithID = $"{penaltyData.PenaltyTypeText}({penaltyData.PenaltyType})";
        }
    }

    var petDataTableService = serviceScope.ServiceProvider.GetRequiredService<PetDataTableService>();
    petDataTableService.LoadTable();

    foreach (var pet in petDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == pet.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            pet.NameString = stringData.SourceString;
            pet.NameStringWithID = $"{stringData.SourceString}({pet.ID})";
        }
        else
        {
            pet.NameString = pet.Name;
            pet.NameStringWithID = $"{pet.Name}({pet.ID})";
        }
    }

    var petAbilityListDataTableService = serviceScope.ServiceProvider.GetRequiredService<PetAbilityListDataTableService>();
    petAbilityListDataTableService.LoadTable();

    var petEggDataTableService = serviceScope.ServiceProvider.GetRequiredService<PetEggDataTableService>();
    petEggDataTableService.LoadTable();

    foreach (var petEgg in petEggDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == petEgg.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            petEgg.NameString = stringData.SourceString;
            petEgg.NameStringWithID = $"{stringData.SourceString}({petEgg.ID})";
        }
        else
        {
            petEgg.NameString = petEgg.Name;
            petEgg.NameStringWithID = $"{petEgg.Name}({petEgg.ID})";
        }
    }

    var petEggGroupDataTableService = serviceScope.ServiceProvider.GetRequiredService<PetEggGroupDataTableService>();
    petEggGroupDataTableService.LoadTable();

    var profileDataTableService = serviceScope.ServiceProvider.GetRequiredService<ProfileDataTableService>();
    profileDataTableService.LoadTable();

    foreach (var profile in profileDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == profile.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            profile.NameString = stringData.SourceString;
            profile.NameStringWithID = $"{stringData.SourceString}({profile.ID})";
        }
        else
        {
            profile.NameString = profile.Name;
            profile.NameStringWithID = $"{profile.Name}({profile.ID})";
        }
    }

    var questDataTableService = serviceScope.ServiceProvider.GetRequiredService<QuestDataTableService>();
    questDataTableService.LoadTable();

    foreach (var quest in questDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == quest.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            quest.NameString = stringData.SourceString;
            quest.NameStringWithID = $"{stringData.SourceString}({quest.ID})";
        }
        else
        {
            quest.NameString = quest.Name;
            quest.NameStringWithID = $"{quest.Name}({quest.ID})";
        }
    }

    var rewardDataTableService = serviceScope.ServiceProvider.GetRequiredService<RewardDataTableService>();
    rewardDataTableService.LoadTable();

    var seasonMissionCountDataTableService = serviceScope.ServiceProvider.GetRequiredService<SeasonMissionCountDataTableService>();
    seasonMissionCountDataTableService.LoadTable();

    var seasonMissionListDataTableService = serviceScope.ServiceProvider.GetRequiredService<SeasonMissionListDataTableService>();
    seasonMissionListDataTableService.LoadTable();

    foreach (var missionList in seasonMissionListDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == missionList.Description);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            missionList.DescriptionString = stringData.SourceString;
            missionList.DescriptionStringWithID = $"{stringData.SourceString}({missionList.ID})";
        }
        else
        {
            missionList.DescriptionString = missionList.Description;
            missionList.DescriptionStringWithID = $"{missionList.Description}({missionList.ID})";
        }

        if (0 < missionList.ReqHeroID)
        {
            var character = characterDataTableService.datas.Find(_ => _.ID == missionList.ReqHeroID);
            if (null == character)
            {
                missionList.CharacterName = missionList.ReqHeroID.ToString();
                missionList.CharacterNameString = missionList.ReqHeroID.ToString();
                missionList.CharacterNameStringWithID = missionList.ReqHeroID.ToString();
            }
            else
            {
                missionList.CharacterName = character.Name;
                missionList.CharacterNameString = character.NameString;
                missionList.CharacterNameStringWithID = character.NameStringWithID;
            }
        }
    }

    var seasonPassDataTableService = serviceScope.ServiceProvider.GetRequiredService<SeasonPassDataTableService>();
    seasonPassDataTableService.LoadTable();

    foreach (var seasonPass in seasonPassDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == seasonPass.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            seasonPass.NameString = stringData.SourceString;
            seasonPass.NameStringWithID = $"{stringData.SourceString}({seasonPass.ID})";
        }
        else
        {
            seasonPass.NameString = seasonPass.Name;
            seasonPass.NameStringWithID = $"{seasonPass.Name}({seasonPass.ID})";
        }
    }

    var seasonPassLevelDataTableService = serviceScope.ServiceProvider.GetRequiredService<SeasonPassLevelDataTableService>();
    seasonPassLevelDataTableService.LoadTable();

    var seasonPassRewardDataTableService = serviceScope.ServiceProvider.GetRequiredService<SeasonPassRewardDataTableService>();
    seasonPassRewardDataTableService.LoadTable();

    var silverMedalStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<SilverMedalStoreDataTableService>();
    silverMedalStoreDataTableService.LoadTable();

    var skillDataTableService = serviceScope.ServiceProvider.GetRequiredService<SkillDataTableService>();
    skillDataTableService.LoadTable();

    var skinDataTableService = serviceScope.ServiceProvider.GetRequiredService<SkinDataTableService>();
    skinDataTableService.LoadTable();

    foreach (var skin in skinDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == skin.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            skin.NameString = stringData.SourceString;
            skin.NameStringWithID = $"{stringData.SourceString}({skin.ID})";
        }
        else
        {
            skin.NameString = skin.Name;
            skin.NameStringWithID = $"{skin.Name}({skin.ID})";
        }
    }

    var userBlockStringDataTableService = serviceScope.ServiceProvider.GetRequiredService<UserBlockStringDataTableService>();
    userBlockStringDataTableService.LoadTable();

    var treasureBoxDataTableService = serviceScope.ServiceProvider.GetRequiredService<TreasureBoxDataTableService>();
    treasureBoxDataTableService.LoadTable();

    var vehicleDataTableService = serviceScope.ServiceProvider.GetRequiredService<VehicleDataTableService>();
    vehicleDataTableService.LoadTable();

    foreach (var vehicle in vehicleDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == vehicle.Description);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            vehicle.DescriptionString = stringData.SourceString;
            vehicle.DescriptionStringWithID = $"{stringData.SourceString}({vehicle.ID})";
        }
        else
        {
            vehicle.DescriptionString = vehicle.Description;
            vehicle.DescriptionStringWithID = $"{vehicle.Description}({vehicle.ID})";
        }
    }

    var weaponCategoryDataTableService = serviceScope.ServiceProvider.GetRequiredService<WeaponCategoryDataTableService>();
    weaponCategoryDataTableService.LoadTable();

    foreach (var weaponCategory in weaponCategoryDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == weaponCategory.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            weaponCategory.NameString = stringData.SourceString;
            weaponCategory.NameStringWithID = $"{stringData.SourceString}({weaponCategory.ID})";
        }
        else
        {
            weaponCategory.NameString = weaponCategory.Name;
            weaponCategory.NameStringWithID = $"{weaponCategory.Name}({weaponCategory.ID})";
        }
    }

    var weaponCategoryUpgradeDataTableService = serviceScope.ServiceProvider.GetRequiredService<WeaponCategoryUpgradeDataTableService>();
    weaponCategoryUpgradeDataTableService.LoadTable();

    var wonderCubeDataTableService = serviceScope.ServiceProvider.GetRequiredService<WonderCubeDataTableService>();
    wonderCubeDataTableService.LoadTable();

    foreach (var wonderCubeData in wonderCubeDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == wonderCubeData.Name);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            wonderCubeData.NameString = stringData.SourceString;
            wonderCubeData.NameStringWithID = $"{stringData.SourceString}({wonderCubeData.ID})";
        }
        else
        {
            wonderCubeData.NameString = wonderCubeData.Name;
            wonderCubeData.NameStringWithID = $"{wonderCubeData.Name}({wonderCubeData.ID})";
        }
    }

    var wonderCubeRwardDataTableService = serviceScope.ServiceProvider.GetRequiredService<WonderCubeRwardDataTableService>();
    wonderCubeRwardDataTableService.LoadTable();

    var wonderStoreDataTableService = serviceScope.ServiceProvider.GetRequiredService<WonderStoreDataTableService>();
    wonderStoreDataTableService.LoadTable();

    foreach (var wonderStoreData in wonderStoreDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == wonderStoreData.ProductName);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            wonderStoreData.ProductNameString = stringData.SourceString;
            wonderStoreData.ProductNameStringWithID = $"{stringData.SourceString}({wonderStoreData.ID})";
        }
        else
        {
            wonderStoreData.ProductNameString = wonderStoreData.ProductName;
            wonderStoreData.ProductNameStringWithID = $"{wonderStoreData.ProductName}({wonderStoreData.ID})";
        }
    }

    var worldMapDataTableService = serviceScope.ServiceProvider.GetRequiredService<WorldMapDataTableService>();
    worldMapDataTableService.LoadTable();

    foreach (var worldMapData in worldMapDataTableService.datas)
    {
        var stringData = stringDataTableService.datas.Find(_ => _.Key == worldMapData.WorldmapPopTitle);
        if (null != stringData && false == string.IsNullOrWhiteSpace(stringData.SourceString))
        {
            worldMapData.WorldmapPopTitleString = stringData.SourceString;
            worldMapData.WorldmapPopTitleStringWithID = $"{stringData.SourceString}({worldMapData.ID})";
        }
        else
        {
            worldMapData.WorldmapPopTitleString = worldMapData.WorldmapPopTitle;
            worldMapData.WorldmapPopTitleStringWithID = $"{worldMapData.WorldmapPopTitle}({worldMapData.ID})";
        }
    }

    var biskitLogEventIDTableService = serviceScope.ServiceProvider.GetRequiredService<BiskitLogEventIDTableService>();
    var selectBiskitLogEventIDsTask = await dbHelperAdmin.SelectBiskitLogEventIDs();
    if (selectBiskitLogEventIDsTask.Item1 && null != selectBiskitLogEventIDsTask.Item2 && 0 < selectBiskitLogEventIDsTask.Item2.Count)
    {
        var biskitLogEventIDDatas = new Dictionary<string, BiskitLogEventID>();
        foreach (var data in selectBiskitLogEventIDsTask.Item2)
        {
            if (false == biskitLogEventIDDatas.TryGetValue(data.EventID, out var existsData))
                biskitLogEventIDDatas.Add(data.EventID, data);
        }

        biskitLogEventIDTableService.SetTable(biskitLogEventIDDatas);
    }

    var blockContentDataTableService = serviceScope.ServiceProvider.GetRequiredService<BlockContentDataTableService>();
    var selectBlockContentsTask = await dbHelperAdmin.SelectBlockContents();
    if (selectBlockContentsTask.Item1 && null != selectBlockContentsTask.Item2 && 0 < selectBlockContentsTask.Item2.Count)
    {
        var blockContentsDatas = new Dictionary<string, BlockContentData>();
        foreach (var data in selectBlockContentsTask.Item2)
        {
            if (false == blockContentsDatas.TryGetValue(data.PacketID, out var existsData))
                blockContentsDatas.Add(data.PacketID, data);
        }

        blockContentDataTableService.SetTable(blockContentsDatas);
    }

    var errorsDataTableService = serviceScope.ServiceProvider.GetRequiredService<ErrorsDataTableService>();
    var errorsDatasFilePath = AppContext.BaseDirectory + "AutoErrors.json";
    if (File.Exists(errorsDatasFilePath))
    {
        try
        {
            var errorsDatasString = File.ReadAllText(errorsDatasFilePath);
            var deserializedErrorsDatas = JsonConvert.DeserializeObject<Dictionary<Errors, ErrorsData>>(errorsDatasString);
            if (deserializedErrorsDatas != null && 0 < deserializedErrorsDatas.Count)
            {
                errorsDataTableService.SetTableVersion((serverStateService.masterVersion * 1000000) + (serverStateService.updateVersion * 10) + serverStateService.maintenanceVersion);
                errorsDataTableService.SetTable(deserializedErrorsDatas);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);
        }
    }

    if (null != navMenuSettings && null != navMenuSettings.navMenus && 0 < navMenuSettings.navMenus.Count)
    {
        var navMenuDataTableService = serviceScope.ServiceProvider.GetRequiredService<NavMenuDataTableService>();
        var navMenuDatas = new Dictionary<string, NavMenuData>();

        foreach (var menu in navMenuSettings.navMenus)
            AddNavMenuData(menu, navMenuDatas);

        navMenuDataTableService.SetTableVersion((serverStateService.masterVersion * 1000000) + (serverStateService.updateVersion * 10) + serverStateService.maintenanceVersion);
        navMenuDataTableService.SetTable(navMenuDatas);
    }
    #endregion

    #region Firewall Service
    var selectFirewallsTask = await dbHelperGameManager.SelectFirewalls();
    if (selectFirewallsTask.Item1 && null != selectFirewallsTask.Item2 && 0 < selectFirewallsTask.Item2.Count)
    {
        var firewallService = serviceScope.ServiceProvider.GetRequiredService<FirewallService>();
        var allowIPAddreses = new List<string>();
        var denyIPAddreses = new List<string>();

        foreach (var firewall in selectFirewallsTask.Item2)
        {
            switch (firewall.method)
            {
                case Defines.FirewallMethod.Allow:
                    allowIPAddreses.Add(firewall.ipAddress);
                    break;

                case Defines.FirewallMethod.Deny:
                    denyIPAddreses.Add(firewall.ipAddress);
                    break;
            }
        }

        firewallService.setAllowIPAddresses(allowIPAddreses);
        firewallService.setDenyIPAddresses(denyIPAddreses);
    }
    #endregion

    var selectPWLogsTask = await dbHelperGMLog.SelectPWLogs();
    if (selectPWLogsTask.Item1 && null != selectPWLogsTask.Item2 && 0 < selectPWLogsTask.Item2.Count)
    {
        foreach (var log in selectPWLogsTask.Item2)
        {
            var newMessage = Regex.Replace(log.message, "\"password\":\"[^\"]+\"", "\"password\":\"\"");
            log.message = newMessage;
            await dbHelperGMLogWriteOnly.UpdateLogMessage(log);
        }
    }
}

app.UseCors(builder =>
{
    var origns = new List<string>();
    foreach (var client in commonSettings.clientInfo)
        origns.Add(client.host);

    builder.WithOrigins(origns.ToArray())
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
});

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};
app.UseCookiePolicy(cookiePolicyOptions);

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.Use(async (context, next) =>
{
    var checkLBOption = new CookieOptions { Path = "/", HttpOnly = true, Secure = true, IsEssential = true, Expires = DateTime.Now.AddHours(24), SameSite = SameSiteMode.None };
    context.Response.Cookies.Append("CheckLB", "CheckLB", checkLBOption);

    var requestDataService = context.RequestServices.GetRequiredService<RequestDataService>();

    var headerServerAddress = context.Request.Headers["ServerAddress"];
    if (string.IsNullOrWhiteSpace(headerServerAddress))
    {
        var localIpAddress = context.Connection.LocalIpAddress;
        if (null != localIpAddress)
            requestDataService.serverAddress = localIpAddress.ToString();
    }
    else
    {
        requestDataService.serverAddress = headerServerAddress.ToString();
    }

    var headerX_Forwarded_For = context.Request.Headers["X-Forwarded-For"];
    if (string.IsNullOrWhiteSpace(headerX_Forwarded_For))
    {
        if (null != context.Connection.RemoteIpAddress)
            requestDataService.remoteAddress = context.Connection.RemoteIpAddress.ToString();
    }
    else
    {
        requestDataService.remoteAddress = headerX_Forwarded_For.ToString();
    }

    var headerX_Forwarded_Proto = context.Request.Headers["X-Forwarded-Proto"];
    if (string.IsNullOrWhiteSpace(headerX_Forwarded_Proto))
    {
        if (false == string.IsNullOrWhiteSpace(context.Request.Scheme))
            requestDataService.scheme = context.Request.Scheme;
    }
    else
    {
        requestDataService.scheme = headerX_Forwarded_Proto.ToString();
    }

    var headerPort = context.Request.Headers["Port"];
    if (string.IsNullOrWhiteSpace(headerPort))
    {
        requestDataService.port = null == context.Request.Host.Port ? 0 : (int)context.Request.Host.Port;
    }
    else
    {
        requestDataService.port = int.Parse(headerPort.ToString());
    }

    //var headerX_Forwarded_Host = context.Request.Headers["X-Forwarded-Host"];
    //if (string.IsNullOrWhiteSpace(headerX_Forwarded_Host))
    //{
    //    requestDataService.host = context.Request.Host.Host;
    //}
    //else
    //{
    //    requestDataService.host = headerX_Forwarded_Host.ToString();
    //}

    //if (0 != requestDataService.port && requestDataService.port != 80 && requestDataService.port != 443)
    //    requestDataService.host = requestDataService.host + ":" + requestDataService.port;

    if (requestDataService.port == 443 || requestDataService.port == 80 || 0 == requestDataService.port)
    {
        requestDataService.host = context.Request.Host.Host;
    }
    else
    {
        requestDataService.host = context.Request.Host.Host + ":" + requestDataService.port;
    }

    var headerUserAgent = context.Request.Headers["User-Agent"];
    if (false == string.IsNullOrWhiteSpace(headerUserAgent))
        requestDataService.userAgent = headerUserAgent.ToString();

    await next();
});

app.MapControllers();
app.MapHub<GMServerHub>("/GMServer");

app.Run();

void MenuRoleToPolicy(AuthorizationOptions options, List<NavMenu> menus)
{
    if (null == menus || 1 > menus.Count)
        return;

    foreach (var menu in menus)
    {
        if (null == menu.roles || 1 > menu.roles.Length || string.IsNullOrWhiteSpace(menu.policyName))
            continue;

        if (commonSettings.isService)
        {
            options.AddPolicy(menu.policyName, policy => policy.RequireRole(menu.roles));
        }
        else
        {
            options.AddPolicy(menu.policyName, policy => policy.RequireAuthenticatedUser());
        }

        if (null != menu.children && 0 < menu.children.Count)
            MenuRoleToPolicy(options, menu.children);
    }
}

async Task InsertRole(List<NavMenu> navMenus, DBHelperGameManager gmDBHelper, DBHelperGameManagerWriteOnly gmWriteOnlyDBHelper)
{
    if (null == navMenus || 1 > navMenus.Count)
        return;

    foreach (var navMenu in navMenus)
    {
        if (null == navMenu.roles || 1 > navMenu.roles.Length)
            continue;

        foreach (var role in navMenu.roles)
        {
            if (string.IsNullOrWhiteSpace(role))
                continue;

            var existsRoleDataTask = await gmDBHelper.SelectRoleByName(role);
            if (true == existsRoleDataTask.Item1 && null != existsRoleDataTask.Item2)
                continue;

            await gmWriteOnlyDBHelper.InsertRole(role);
        }

        if (null != navMenu.children && 0 < navMenu.children.Count)
            await InsertRole(navMenu.children, gmDBHelper, gmWriteOnlyDBHelper);
    }
}

void AddNavMenuData(NavMenu navMenu, Dictionary<string, NavMenuData> navMenuDatas)
{
    if (false == navMenuDatas.TryGetValue(navMenu.path, out var existsData))
        navMenuDatas.Add(navMenu.path, new NavMenuData { Path = navMenu.path, Name = navMenu.name });

    if (0 < navMenu.children.Count)
    {
        foreach (var child in navMenu.children)
            AddNavMenuData(child, navMenuDatas);
    }
}