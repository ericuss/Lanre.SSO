// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models.Consent
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => this.RedirectUri != null;

        public string RedirectUri { get; set; }

        public string ClientId { get; set; }

        public bool ShowView => this.ViewModel != null;

        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => this.ValidationError != null;

        public string ValidationError { get; set; }
    }
}
