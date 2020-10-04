using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "" }, OperationId = "" )]
        public async Task<IActionResult> AuthenticateUser()
        {
            return Ok();
        }
    }
}