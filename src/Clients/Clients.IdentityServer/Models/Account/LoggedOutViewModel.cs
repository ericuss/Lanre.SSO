// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models.Accounts
{
    public class LoggedOutViewModel
    {
        public string PostLogoutRedirectUri { get; set; }

        public string ClientName { get; set; }

        public string SignOutIframeUrl { get; set; }

        public bool AutomaticRedirectAfterSignOut { get; set; }

        public string LogoutId { get; set; }

        public bool TriggerExternalSignout => this.ExternalAuthenticationScheme != null;

        public string ExternalAuthenticationScheme { get; set; }
    }
}