using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.Infrastructure.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate requestDelegate, IOptions<JwtSettings> options)
        {
            _requestDelegate = requestDelegate;
            _jwtSettings = options.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            if(token != null)
            {
                AttachUserToContext(context, userService, token);
            }

            await _requestDelegate(context);
        }

        private void AttachUserToContext(HttpContext httpContext, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters 
                { 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                httpContext.Items["User"] = userId;

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
