using EventYojana.Infrastructure.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace EventYojana.Infrastructure.Core.Interfaces
{
    public interface IRequestContext
    {
        JwtSecurityToken JwtSecurityToken { get; }
        UserSettings LogOnUserDetails { get; }
        
    }
}
