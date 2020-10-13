using System.Threading.Tasks;
using EventYojana.API.Vendor.Constants;
using EventYojana.Infrastructure.Core.Attributes;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using EventYojana.Infrastructure.Core.Models;
using EventYojana.Infrastructure.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventYojana.API.Vendor.Controllers
{
    /// <summary>
    /// Login Vendor
    /// </summary>
    [Route("api/Vendor/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.VendorAuthenticate) )]
        //public async Task<IActionResult> AuthenticateUser([ModelBinder(typeof(FromEncryptedBodyAttribute))] AuthenticateRequest authenticateRequestModel)
        public async Task<IActionResult> AuthenticateUser(AuthenticateRequest authenticateRequestModel)
        {
            ValidationException validationException = new ValidationException();
            validationException.Add(nameof(authenticateRequestModel.UserName), authenticateRequestModel.UserName, ValidationReason.Required);
            validationException.Add(nameof(authenticateRequestModel.Password), authenticateRequestModel.Password, ValidationReason.Required);
            if(validationException.HasErrors)
            {
                throw validationException;
            }

            var result = _userService.Authenticate(authenticateRequestModel);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetUserDetails();
            return Ok(users);
        }
    }
}