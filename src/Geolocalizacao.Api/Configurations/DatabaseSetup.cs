using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Geolocalizacao.Api.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString), x => x.UseNetTopologySuite()));

            services.Configure<NoSqlAppConfig>(configuration.GetSection("NoSqlConnection"));
            services.AddTransient<NoSqlDbContext>();

            services.AddMemoryCache();
        }
    }
}