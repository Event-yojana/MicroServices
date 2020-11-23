using System.Threading.Tasks;
using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common;
using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.Vendor.Constants;
using EventYojana.Infrastructure.Core.Attributes;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventYojana.API.Vendor.Controllers
{
    /// <summary>
    /// Vendor login controller
    /// </summary>
    [Route("api/Vendor/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Vendor login controller 
        /// </summary>
        /// <param name="userService"></param>
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Authenticate vendor user
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

            var result = await _userService.Authenticate(authenticateRequestModel);

            return Ok(result);
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
            validationException.Add(nameof(vendorDetailsRequestModel.UserName), vendorDetailsRequestModel.UserName, ValidationReason.Required);
            validationException.Add(nameof(vendorDetailsRequestModel.Password), vendorDetailsRequestModel.Password, ValidationReason.Required);
            if (validationException.HasErrors)
            {
                throw validationException;
            }

            var result = await _userService.RegisterVendor(vendorDetailsRequestModel);

            return Ok(result);
        }
    }
}