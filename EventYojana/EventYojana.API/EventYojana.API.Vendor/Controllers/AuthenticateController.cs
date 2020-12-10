using System.Threading.Tasks;
using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Vendor;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.BusinessLayer.Interfaces.Vendor;
using EventYojana.API.Vendor.Constants;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

namespace EventYojana.API.Vendor.Controllers
{

    [Route("api/Vendor/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IVendorAuthenticationManager _vendorAuthenticationManager;

        public AuthenticateController(IUserService userService, IVendorAuthenticationManager vendorAuthenticationManager)
        {
            _userService = userService;
            _vendorAuthenticationManager = vendorAuthenticationManager;
        }

        /// <summary>
        /// Register vendor user
        /// </summary>
        /// <param name="vendorDetailsRequestModel"></param>
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.RegisterVendor))]
        public async Task<IActionResult> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel)
        {
            ValidationException validationException = new ValidationException();
            validationException.Add(nameof(vendorDetailsRequestModel.VendorName), vendorDetailsRequestModel.VendorName, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.VendorEmail), vendorDetailsRequestModel.VendorEmail, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.VendorMobile), vendorDetailsRequestModel.VendorMobile, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.AddressLine), vendorDetailsRequestModel.AddressLine, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.City), vendorDetailsRequestModel.City, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.State), vendorDetailsRequestModel.State, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.PinCode), vendorDetailsRequestModel.PinCode, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.VendorEmail), vendorDetailsRequestModel.VendorEmail, ValidationReason.EmailFormat);
            validationException.Add(nameof(vendorDetailsRequestModel.VendorMobile), vendorDetailsRequestModel.VendorMobile, ValidationReason.PhoneNumber);
            validationException.Add(nameof(vendorDetailsRequestModel.PinCode), vendorDetailsRequestModel.PinCode, ValidationReason.PinCode);

            if (validationException.HasErrors)
            {
                throw validationException;
            }

            return Ok(await _vendorAuthenticationManager.RegisterVendor(vendorDetailsRequestModel));
        }

        /// <summary>
        /// Authenticate vendor user
        /// </summary>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.VendorAuthenticate) )]
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