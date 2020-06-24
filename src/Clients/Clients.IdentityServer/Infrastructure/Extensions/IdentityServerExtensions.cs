// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Clients.IdentityServer;
    using Clients.IdentityServer.Data;
    using Clients.IdentityServer.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(connectionString))
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>()

                // not recommended for production - you need to store your key material somewhere secure
                .AddDeveloperSigningCredential()
                .Services
                .AddAuthentication()
                ;

            return services;
        }

        public static IApplicationBuilder UseCustomIdentityServer(this IApplicationBuilder app)
        {
            return app
                    .UseIdentityServer()
                    .UseAuthentication()
                    .UseAuthorization();
        }
    }
}