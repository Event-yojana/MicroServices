using EventYojana.Infrastructure.Core.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventYojana.Infrastructure.Core.Filters.SwaggerFilters
{
    public class UnSecureHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterDescription = context.ApiDescription.CustomAttributes();

            bool isUnscureFilter = filterDescription.Any(x => x is UnSecureheaderAttribute);

            if (isUnscureFilter)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = FilterConstants.XIsUnSecureSwaggerParameter,
                    In = ParameterLocation.Header,
                    Required = false,
                    Schema = new OpenApiSchema()
                    {
                        Default = new OpenApiBoolean(true),
                        Type = "Boolean"
                    }
                });
            }
        }
    }
}
