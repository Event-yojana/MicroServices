using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.Interfaces.Common;
using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Helpers;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Managers.Commons
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _appSettings;

        private readonly IAuthenticateRepository _authenticateRepository;

        public UserService(IOptions<JwtSettings> options, IAuthenticateRepository authenticateRepository)
        {
            _appSettings = options.Value;
            _authenticateRepository = authenticateRepository;
        }
        public async Task<GetResponseModel> Authenticate(string UserName, string Password, int[] UserType)
        {
            GetResponseModel getResponseModel = new GetResponseModel();

            var userDetails = await _authenticateRepository.GetUserDetails(x => x.Username == UserName && UserType.Contains(x.UserType));

            if (userDetails == null && userDetails.Password != AuthenticateUtility.GeneratePassword(Password, userDetails.PasswordSalt))
            {
                getResponseModel.NoContent = true;
            }
            else
            {
                var token = GenerateJwtToken(userDetails);

                AuthenticateResponse authenticateResponse = new AuthenticateResponse()
                {
                    Token = token
                };
                getResponseModel.Content = authenticateResponse;
            }
            getResponseModel.Success = true;
            return getResponseModel;
        }

        private string GenerateJwtToken(UserLogin user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            var authClaims = new List<Claim>
                {
                    new Claim("id", user.LoginId.ToString()),
                    new Claim("role", user.UserType.ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            var token = new JwtSecurityToken(
                    issuer: _appSettings.ValidIssuer,
                    audience: _appSettings.ValidAudience,
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

            return tokenHandler.WriteToken(token);
        }
    }
}
