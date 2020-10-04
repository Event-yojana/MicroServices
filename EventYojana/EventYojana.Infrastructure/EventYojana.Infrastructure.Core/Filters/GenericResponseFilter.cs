using EventYojana.Infrastructure.Core.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Filters
{
    public class GenericResponseFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(operation.Responses == null)
            {
                operation.Responses = new OpenApiResponses();
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.Success).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.Success).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.Success] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.BadRequest).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.BadRequest).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.BadRequest] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.UnAuthorized).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.UnAuthorized).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.UnAuthorized] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.Forbidden).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.Forbidden).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.Forbidden] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.NotFound).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.NotFound).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.NotFound] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.InternalError).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.InternalError).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.InternalError] });
            }
            if (!operation.Responses.ContainsKey(((int)Responses.ResponseCode.NoContent).ToString()))
            {
                operation.Responses.Add(((int)Responses.ResponseCode.NoContent).ToString(),
                    new OpenApiResponse { Description = Responses.ResponseMessage[(int)Responses.ResponseCode.NoContent] });
            }
        }
    }
}
