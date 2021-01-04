using EventYojana.Infrastructure.Core.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromFormBodyAttribute : Attribute, IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.ActionContext.HttpContext.Request;
            var values = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if(values.Length > 0)
            {
                bool isUnSecure = false;
                var isUnsecureHeader = request.Headers["isUnSecure"];
                if(!string.IsNullOrEmpty(isUnsecureHeader))
                {
                    bool.TryParse(isUnsecureHeader.ToString(), out isUnSecure);
                }

                var value = isUnSecure ? values.FirstValue : AesDescryptionHelper.Decrypt(WebUtility.UrlDecode(values.FirstValue.Replace('!', '%')));

                if(!bindingContext.ModelType.IsPrimitive && bindingContext.ModelType != typeof(decimal) && bindingContext.ModelType != typeof(string))
                {
                    bindingContext.Result = ModelBindingResult.Success(
                        JsonConvert.DeserializeObject(value, bindingContext.ModelType));
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(bindingContext.ModelType == typeof(string) ? value : Convert.ChangeType(value, bindingContext.ModelType));
                }
            }

            await Task.CompletedTask;
        }
    }
}
