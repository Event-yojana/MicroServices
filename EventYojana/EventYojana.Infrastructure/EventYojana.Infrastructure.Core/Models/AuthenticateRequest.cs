using System;
using System.Collections.Generic;
using System.Text;
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

namespace EventYojana.Infrastructure.Core.Models
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRoleEnum UserType { get; set; }
    }
}
