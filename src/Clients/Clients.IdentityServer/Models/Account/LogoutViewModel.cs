// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models.Accounts
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
