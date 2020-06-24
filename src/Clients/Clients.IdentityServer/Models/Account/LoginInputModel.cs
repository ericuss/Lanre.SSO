// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models.Accounts
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}