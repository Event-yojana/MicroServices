﻿using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using static EventYojana.Infrastructure.Core.Models.Responses;

namespace EventYojana.Infrastructure.Core.Attributes
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var userToken = (AuthenticateRequest)context.HttpContext.Items["User"];
    //        if(userToken == null)
    //        {
    //            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = (int)ResponseCode.UnAuthorized };
    //        }
    //    }
    //}
}
