using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
