using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Models;
using EventYojana.Infrastructure.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EventYojana.Infrastructure.Core.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _appSettings;

        private IList<AuthenticateRequest> authenticateRequests = new List<AuthenticateRequest>()
        {
            new AuthenticateRequest{ UserName = "Test", Password = "Test" }
        };

        public UserService(IOptions<JwtSettings> options)
        {
            _appSettings = options.Value;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = authenticateRequests.SingleOrDefault(x => x.UserName == authenticateRequest.UserName && x.Password == authenticateRequest.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            //Mapper
            AuthenticateResponse authenticateResponse = new AuthenticateResponse()
            {
                Token = token
            };

            return authenticateResponse;

        }

        public IList<AuthenticateRequest> GetUserDetails()
        {
            return authenticateRequests;
        }

        private string generateJwtToken(AuthenticateRequest user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
