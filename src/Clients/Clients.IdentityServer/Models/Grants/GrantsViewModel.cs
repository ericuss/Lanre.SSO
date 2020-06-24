// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models.Grants
{
    using System;
    using System.Collections.Generic;

    public class GrantsViewModel
    {
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }
}