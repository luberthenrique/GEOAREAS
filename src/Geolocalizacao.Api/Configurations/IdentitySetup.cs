using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Infra.Data.Context;
using Geolocalizacao.Application.Services.Configuration;
using Geolocalizacao.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Security.Claims;

namespace Geolocalizacao.Api.Configurations
{
    public static class IdentitySetup
    {
        public static void AddIdentitySetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<MenuHelper>();

            // JWT Setup
            services.AddSingleton<JwtSettingService>(sp =>
            {
                return new JwtSettingService(
                    configuration["JwtSettings:Secret"],
                    Convert.ToInt32(configuration["JwtSettings:Expiration"]),
                    configuration["JwtSettings:Issuer"],
                    configuration["JwtSettings:ValidAt"]);
            });

            var jwtSettings = services.BuildServiceProvider().GetService<JwtSettingService>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidAudience = appSettings.ValidAt,
                    //ValidIssuer = appSettings.Issuer
                };
            });

            AddAuthSetup(services, configuration);

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireRole("client")
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

        }

        public static void AddAuthSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            services.AddAuthorization(options =>
            {
                var menuService = services.BuildServiceProvider().GetService<MenuHelper>();
                var menus = menuService.GetMenus();

                var clains = menuService.GetClaims();

                foreach (var claim in clains)
                {
                    options.AddPolicy($"{claim.Value}{claim.Type}", policy => policy.RequireClaim(claim.Type, claim.Value));
                }
            });
        }
    }
}