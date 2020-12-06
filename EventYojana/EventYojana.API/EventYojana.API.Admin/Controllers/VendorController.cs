using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.API.Admin.Constants;
using EventYojana.API.BusinessLayer.Interfaces.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventYojana.API.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorManager _vendorManager;
        /// <summary>
        /// 
        /// </summary>
        public VendorController(IVendorManager vendorManager)
        {
            _vendorManager = vendorManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetRegisteredVendors")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Vendor }, OperationId = nameof(SwaggerOperation.RequestForRegister))]
        public async Task<IActionResult> NotRegisteredVendors()
        {
            return Ok(await _vendorManager.GetRegisteredVendorsList());
        }
    }
}
