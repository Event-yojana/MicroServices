using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventYojana.Infrastructure.Core.Interfaces;
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
        public readonly string LogOnUserId;
        /// <summary>
        /// Common constructure
        /// </summary>
        protected BaseController(IRequestContext ctx)
        {
            this.LogOnUserId = ctx.LogOnUserId;
        }
    }
}