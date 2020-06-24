// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private const string APPINSIGHTSINSTRUMENTATIONKEY = "ApplicationInsights:InstrumentationKey";
        private const string HTTPSPORT = "HTTPS_PORT";
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            this._configuration = configuration;
            this._env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var (httpsPort, applicationInsightsKey) = this.RegisterConfigurations(services);

            services

                // .AddMediatR()//Assembly.GetAssembly(typeof(GetBooksQuery)))
                .AddApplicationInsightsTelemetry(x => x.InstrumentationKey = applicationInsightsKey)
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                .AddCustomFixForHttps()
                .AddCustomHttps(httpsPort, this._env)
                .AddHttpContextAccessor()
                .AddCustomApiVersion()
                .AddCustomIdentityServer(this._configuration.GetConnectionString("DefaultConnection"))
                .AddControllersWithViews()
                    .AddNewtonsoftJson()

                // .AddApiExplorer()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseCustomFixForHttps()
                .UseCustomTracing()
                .AddIf(env.IsDevelopment(), x => x.UseDeveloperExceptionPage())
                .UseFileServer()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseCustomSwagger()
                .UseCustomIdentityServer()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapDefaultControllerRoute()
                        .RequireAuthorization();
                })

               // .UseWelcomePage()
               ;
        }

        private (int, string) RegisterConfigurations(IServiceCollection services)
        {
            var httpsPort = this._configuration.GetValue<int>(HTTPSPORT);
            var instrumentationKey = this._configuration.GetValue<string>(APPINSIGHTSINSTRUMENTATIONKEY);

            return (httpsPort, instrumentationKey);
        }
    }
}
