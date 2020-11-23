using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventYojana.API.Vendor.Controllers
{
    /// <summary>
    /// Common controller
    /// </summary>
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Common constructure
        /// </summary>
        protected BaseController()
        {

        }
    }
}