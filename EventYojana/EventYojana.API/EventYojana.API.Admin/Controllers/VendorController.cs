using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.API.Admin.Constants;
using EventYojana.API.BusinessLayer.Interfaces.Admin;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using EventYojana.Infrastructure.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventYojana.API.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class VendorController : BaseController
    {
        private readonly IVendorManager _vendorManager;
        
        public VendorController(IRequestContext ctx, IVendorManager vendorManager):base(ctx)
        {
            _vendorManager = vendorManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetRegisteredVendors")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.ListOfRegisteredVendor))]
        public async Task<IActionResult> NotRegisteredVendors()
        {
            return Ok(await _vendorManager.GetRegisteredVendorsList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Confirm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.ConfirmRegistration))]
        public async Task<IActionResult> ConfirmRegistration(int vendorId)
        {
            ValidationException validationException = new ValidationException();
            validationException.Add(nameof(vendorId), vendorId, ValidationReason.GreaterThanZero);

            if (validationException.HasErrors)
            {
                throw validationException;
            }

            return Ok(await _vendorManager.ConfirmRegistration(vendorId));
        }

    }
}
