using Geolocalizacao.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;

namespace Geolocalizacao.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                                var logger = services.GetRequiredService<ILogger<Program>>();

                var sw = new Stopwatch();

                sw.Start();

                logger.LogInformation("Iniciando configurações banco de dados");

                try
                {
                    services.GetRequiredService<NoSqlDbContext>().Initialize();
                }
                catch (Exception ex)
                {
                }

                logger.LogInformation("Finalizando configurações banco de dados.");

                sw.Stop();

                logger.LogInformation($"Finalizando procedimentos básicos : {sw.ElapsedMilliseconds} milissegundos");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.
                    ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;

                        if (env.IsEnvironment("Debug"))
                            env.EnvironmentName = "Development";

                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
                        config.AddEnvironmentVariables();

                        var levelSwitch = new LoggingLevelSwitch
                        {
                            MinimumLevel = ObterConfiguracaoLog()
                        };

                        Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .MinimumLevel.ControlledBy(levelSwitch)
                            //.WriteTo.AddAzureWebAppDiagnostics(Path.Combine(caminhoLog, $"log-{DateTime.Now.Date.ToString("yyyyMMdd")}.txt"))
                            .CreateLogger();
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddSerilog(dispose: true);
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddAzureWebAppDiagnostics();
                    })
                    .ConfigureServices(services =>
                    {
                        services.Configure<AzureFileLoggerOptions>(options =>
                        {
                            options.FileName = $"log-azure-{DateTime.Now.Date.ToString("yyyyMMdd")}";
                            options.FileSizeLimit = 50 * 1024;
                            options.RetainedFileCountLimit = 5;
                        });
                    })
                    .UseStartup<Startup>();
                });
        }

        private static LogEventLevel ObterConfiguracaoLog()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var nivel = config.GetSection("Logging").GetSection("LogLevel")["Default"].ToUpper();

            LogEventLevel retorno;

            switch (nivel)
            {
                case "VERBOSE":
                    retorno = LogEventLevel.Verbose;
                    break;

                case "DEBUG":
                    retorno = LogEventLevel.Debug;
                    break;

                case "INFORMATION":
                    retorno = LogEventLevel.Information;
                    break;

                case "WARNING":
                    retorno = LogEventLevel.Warning;
                    break;

                case "FATAL":
                    retorno = LogEventLevel.Fatal;
                    break;

                case "ERROR":
                    retorno = LogEventLevel.Error;
                    break;
                default:
                    retorno = LogEventLevel.Warning;
                    break;
            }

            return retorno;

        }
    }
}
