using EventYojana.Infrastructure.Core.Enum;
using EventYojana.Infrastructure.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace EventYojana.Infrastructure.Core.Helpers
{
    public sealed class RequestContextAdapter : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContextAdapter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public SecurityEnum.UserRoleEnum UserRole { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public JwtSecurityToken JwtSecurityToken
        {
            get
            {
                var jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                if(jwt.Count > 0)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt.ToString().Split(' ').Last());
                    return token;
                }
                return new JwtSecurityToken();
            }
        }

        public string LogOnUserId
        {
            get
            {
                if(JwtSecurityToken.Claims.Count() > 0)
                {
                    return JwtSecurityToken.Claims.First(x => x.Type == "id").Value;
                }
                return null;
            }
        }
    }
}
