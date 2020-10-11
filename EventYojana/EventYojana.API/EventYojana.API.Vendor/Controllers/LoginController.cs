using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common;
using EventYojana.Infrastructure.Core.Attributes;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public LoginController()
        {

        }
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "" }, OperationId = "" )]
        public async Task<IActionResult> AuthenticateUser([ModelBinder(typeof(FromEncryptedBodyAttribute))] AuthenticateRequestModel authenticateRequestModel)
        {
            ValidationException validationException = new ValidationException();
            validationException.Add(nameof(authenticateRequestModel.UserName), authenticateRequestModel.UserName, ValidationReason.Required);
            validationException.Add(nameof(authenticateRequestModel.Password), authenticateRequestModel.Password, ValidationReason.Required);
            if(validationException.HasErrors)
            {
                throw validationException;
            }

            return Ok();
        }
    }
}