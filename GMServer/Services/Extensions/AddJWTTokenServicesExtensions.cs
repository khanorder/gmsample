using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NGEL.Data.Settings;

namespace GMServer.Services.Extensions
{
    public static class AddJWTTokenServicesExtensions
    {
        public static void AddJWTTokenServices(this IServiceCollection Services, ConfigurationManager Configuration, IWebHostEnvironment Environment)
        {
            // Add Jwt Settings
            if (Environment.IsDevelopment())
            {
                Configuration.AddJsonFile("jwtsettings.Development.json", optional: false, reloadOnChange: false);
            }
            else
            {
                Configuration.AddJsonFile("jwtsettings.json", optional: false, reloadOnChange: false);
            }

            var JwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            Services.AddSingleton(JwtSettings);


            Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = JwtSettings.validateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.issuerSigningKey)),
                        ValidateIssuer = JwtSettings.validateIssuer,
                        ValidIssuer = JwtSettings.validIssuer,
                        ValidateAudience = JwtSettings.validateAudience,
                        ValidAudiences = JwtSettings.validAudiences,
                        RequireExpirationTime = JwtSettings.requireExpirationTime,
                        ValidateLifetime = JwtSettings.validateLifetime,
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken? securityToken, TokenValidationParameters? validationParameters) =>
                        {
                            return notBefore <= DateTime.UtcNow && expires > DateTime.UtcNow;
                        },
                        ClockSkew = TimeSpan.Zero,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (false == string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments("/GMServer"))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
