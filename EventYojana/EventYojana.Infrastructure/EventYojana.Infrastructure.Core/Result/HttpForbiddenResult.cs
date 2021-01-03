using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using static EventYojana.Infrastructure.Core.Models.Responses;

namespace EventYojana.Infrastructure.Core.Result
{
    public class HttpForbiddenResult: ActionResult
    {
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)ResponseCode.Forbidden;

            string jsonString = ResponseMessage[(int)ResponseCode.Forbidden];
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonString);

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
