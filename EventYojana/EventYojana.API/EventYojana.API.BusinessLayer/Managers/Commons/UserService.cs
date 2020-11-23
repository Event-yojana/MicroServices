using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.API.BusinessLayer.Constants;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
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
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

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
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var userDetails = await _authenticateRepository.GetUserDetails(x => x.Username == authenticateRequest.UserName && x.UserType == (int)authenticateRequest.UserType);
            
            if (userDetails == null) return null;

            if(userDetails.Password == AuthenticateUtility.GeneratePassword(authenticateRequest.Password, userDetails.PasswordSalt))
            {
                var token = GenerateJwtToken(userDetails);

                AuthenticateResponse authenticateResponse = new AuthenticateResponse()
                {
                    Token = token
                };
                return authenticateResponse;
            }
            return null;
        }

        public async Task<PostResponseModel> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel)
        {
            PostResponseModel postResponseModel = new PostResponseModel();

            var isUserExists = await _authenticateRepository.IsUserDetails(x => x.Username == vendorDetailsRequestModel.UserName);
            postResponseModel.IsAlreadyExists = isUserExists;

            if (!postResponseModel.IsAlreadyExists)
            {
                RegisterVendor registerUser = new RegisterVendor()
                {
                    VendorName = vendorDetailsRequestModel.VendorName,
                    VendorEmail = vendorDetailsRequestModel.VendorEmail,
                    Username = vendorDetailsRequestModel.UserName,
                    UserType = (int)UserRoleEnum.Vendor.GetTypeCode(),
                    PasswordSalt = AuthenticateUtility.CreatePasswordSalt()
                };

                registerUser.Password = AuthenticateUtility.GeneratePassword(vendorDetailsRequestModel.Password, registerUser.PasswordSalt);
                postResponseModel.Success = await _authenticateRepository.RegisterVendor(registerUser);
            }

            return postResponseModel;
        }

        private string GenerateJwtToken(UserLogin user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("ID", EncryptDcryptData.EncryptString(user.LoginId.ToString())), new Claim("USERTYPE", EncryptDcryptData.EncryptString(user.UserType.ToString())) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
