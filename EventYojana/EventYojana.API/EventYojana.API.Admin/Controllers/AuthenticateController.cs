using System.Threading.Tasks;
using EventYojana.API.Admin.Constants;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.Infrastructure.Core.Attributes;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using EventYojana.Infrastructure.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static EventYojana.Infrastructure.Core.Enum.ApplicationEnum;

namespace EventYojana.API.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController
    {
        private readonly IUserService _userService;

        public AuthenticateController(IRequestContext ctx, IUserService userService):base(ctx)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [Route("Authenticate/{UserName}/{Password}")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Admin }, OperationId = nameof(SwaggerOperation.AuthenticateAdmin))]
        public async Task<IActionResult> AuthenticateUser([ModelBinder(typeof(FromEncryptedRouteAttribute))] string UserName, [ModelBinder(typeof(FromEncryptedRouteAttribute))] string Password)
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

            int[] roles = new int[] { (int)UserRoleEnum.Admin, (int)UserRoleEnum.SuperAdmin };
            var result = await _userService.Authenticate(UserName, Password, roles);

            if (result.NoContent)
                return Unauthorized();

            return Ok(result);
        }
    }
}
