using EventYojana.Infrastructure.Core.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventYojana.Infrastructure.Core.Filters.SwaggerFilters
{
    public class FileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterDescription = context.ApiDescription.ActionDescriptor.FilterDescriptors.Select(filterInfo => filterInfo.Filter).ToList();

            bool isConsumesFilter = filterDescription.Any(x => x is ConsumesFileAttribute);
            if(isConsumesFilter)
            {
                operation.RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>()
                    {
                        {
                            "multipart/form-data", new OpenApiMediaType()
                            {
                                Schema = new OpenApiSchema()
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>()
                                    {
                                        {
                                            "file", new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Format = "binary"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}
