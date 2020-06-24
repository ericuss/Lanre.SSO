// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Clients.IdentityServer.Infrastructure;
    using Clients.IdentityServer.Models.Diagnostics;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", this.HttpContext.Connection.LocalIpAddress.ToString() };
            if (!localAddresses.Contains(this.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return this.NotFound();
            }

            var model = new DiagnosticsViewModel(await this.HttpContext.AuthenticateAsync());
            return this.View(model);
        }
    }
}