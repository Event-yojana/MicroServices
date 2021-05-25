using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.Infrastructure.Core.Interfaces;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventYojana.API.Admin.Controllers
{
    /// <summary>
    /// Common controller
    /// </summary>
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        public readonly UserSettings LogOnUserDetails;
        /// <summary>
        /// Common constructure
        /// </summary>
        protected BaseController(IRequestContext ctx)
        {
            this.LogOnUserDetails = ctx.LogOnUserDetails;
        }

    }
}