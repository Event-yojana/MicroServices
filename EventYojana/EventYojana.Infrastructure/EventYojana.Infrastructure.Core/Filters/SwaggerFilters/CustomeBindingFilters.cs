using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EventYojana.Infrastructure.Core.Filters.SwaggerFilters
{
    public class CustomeBindingFilters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var allParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(x => x.BindingInfo?.BinderType != null && x.BindingInfo.BinderType.FullName.StartsWith("EventYojana.Infrastructure.Core.Attributes")).ToList();

            int? customAttributesCount = allParameters.Select(x => x.BindingInfo?.BinderType?.FullName)?.Count();

            if(customAttributesCount.GetValueOrDefault() > 0)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                foreach(var par in allParameters)
                {
                    string attribute = par.BindingInfo.BinderType.FullName;
                    switch(attribute)
                    {
                        case "EventYojana.Infrastructure.Core.Attributes.FromPayloadHeaderAttribute":
                            {
                                var selectedRederence = operation.Parameters.FirstOrDefault(x => x.Name == par.Name);

                                operation.Parameters.Add(new OpenApiParameter
                                {
                                    Name = "Payload",
                                    In = ParameterLocation.Header,
                                    Description = "Payload for get request",
                                    Required = false,
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "String",
                                        Default = new OpenApiString(JsonConvert.SerializeObject(getObject(par.ParameterType)))
                                    }
                                });
                                operation.Parameters.Remove(selectedRederence);
                            }
                            break;

                        case "EventYojana.Infrastructure.Core.Attributes.FromEncryptedRouteAttribute":
                            {
                                var parameters = operation.Parameters.Where(x => x.In == ParameterLocation.Query).ToList();
                                parameters.ForEach(x => x.In = ParameterLocation.Path);
                            }
                            break;

                        case "EventYojana.Infrastructure.Core.Attributes.FromEncryptedBodyAttribute":
                            {
                                var selectedReference = operation.Parameters.FirstOrDefault(x => x.Name == par.Name);

                                operation.RequestBody = new OpenApiRequestBody()
                                {
                                    Content = new Dictionary<string, OpenApiMediaType>()
                                    {
                                        {
                                            "application/json", new OpenApiMediaType()
                                            {
                                                    Schema = new OpenApiSchema()
                                                    {
                                                        Reference = selectedReference?.Schema.Reference
                                                    }
                                            }
                                        }
                                    }
                                };
                                operation.Parameters.Remove(selectedReference);
                            }
                            break;
                    }
                }
            }
        }

        private static object getObject(Type ParameterType)
        {
            var obj = Activator.CreateInstance(ParameterType);
            if(obj == null)
            {
                return null;
            }
            var properties = ParameterType.GetProperties();
            foreach(PropertyInfo propertyInfo in properties)
            {
                Type currentType = propertyInfo.PropertyType;
                var propertyData = propertyInfo.GetValue(obj, null);
                if(currentType == typeof(string))
                {
                    propertyInfo.SetValue(obj, "string");
                }
                else if(propertyData == null && !currentType.IsPrimitive)
                {
                    propertyInfo.SetValue(obj, getObject(currentType));
                }
            }
            return obj;
        }
    }
}
