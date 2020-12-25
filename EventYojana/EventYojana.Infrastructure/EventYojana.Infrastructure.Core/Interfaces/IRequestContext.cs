using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

namespace EventYojana.Infrastructure.Core.Interfaces
{
    public interface IRequestContext
    {
        JwtSecurityToken JwtSecurityToken { get; }
        string LogOnUserId { get; }
        UserRoleEnum UserRole { get; }
    }
}
