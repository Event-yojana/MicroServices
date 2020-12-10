using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.API.Admin.Constants;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

namespace EventYojana.API.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Authenticate vendor user
        /// </summary>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.AuthenticateAdmin))]
        //public async Task<IActionResult> AuthenticateUser([ModelBinder(typeof(FromEncryptedBodyAttribute))] AuthenticateRequest authenticateRequestModel)
        public async Task<IActionResult> AuthenticateUser(string UserName, string Password)
        {
            ValidationException validationException = new ValidationException();
            validationException.Add(nameof(UserName), UserName, ValidationReason.Required);
            validationException.Add(nameof(Password), Password, ValidationReason.Required);
            validationException.Add(nameof(UserName), UserName, ValidationReason.Username);
            validationException.Add(nameof(Password), Password, ValidationReason.PasswordFormat);
            if (validationException.HasErrors)
            {
                throw validationException;
            }

            var result = await _userService.Authenticate(UserName, Password, (int)UserRoleEnum.Vendor);

            if (result.NoContent)
                return Unauthorized();

            return Ok(result);
        }
    }
}
