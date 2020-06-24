// Copyright (c) Lanre. All rights reserved.

namespace Clients.IdentityServer.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string error)
        {
            this.Error = new ErrorMessage { Error = error };
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public ErrorMessage Error { get; set; }
    }
}
