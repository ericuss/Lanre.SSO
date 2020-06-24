// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }

                Log.Information("Starting build...");
                var host = CreateHostBuilder(args).Build();

                if (seed)
                {
                    Log.Information("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var connectionString = config.GetConnectionString("DefaultConnection");
                    SeedData.EnsureSeedData(connectionString);
                    Log.Information("Done seeding database.");
                    return 0;
                }

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((env, configBuilder) =>
                {
                    if (env.HostingEnvironment.IsDevelopment())
                    {
                        configBuilder.AddUserSecrets<Startup>(optional: true);
                    }

                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseSerilog((builderContext, config) => CreateSerilogLogger(builderContext, config))
                        .UseStartup<Startup>();
                });

        private static void CreateSerilogLogger(WebHostBuilderContext builderContext, LoggerConfiguration config)
        {
            var instrumentationKey = builderContext.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

            // var configuration = ConfigureBuilder.GetConfiguration(builderContext.HostingEnvironment);
            // var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            config
                .ReadFrom.Configuration(builderContext.Configuration)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Events, Serilog.Events.LogEventLevel.Information)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, Serilog.Events.LogEventLevel.Information)
                ;
        }
    }
}
