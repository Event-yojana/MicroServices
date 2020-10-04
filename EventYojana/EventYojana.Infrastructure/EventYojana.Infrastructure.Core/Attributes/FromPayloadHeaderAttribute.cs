using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromPayloadHeaderAttribute : Attribute, IModelBinder
    {
        public IHttpContextAccessor Accessor { get; set; }
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bool isUnSecure = false;
            var request = bindingContext.ActionContext.HttpContext.Request;
            var isUnsecureHeader = request.Headers["X-isUnSecureSwagger"];
            if(!string.IsNullOrEmpty(isUnsecureHeader))
            {
                bool.TryParse(isUnsecureHeader.ToString(), out isUnSecure);
            }

            var accessor = bindingContext.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            if(accessor.HttpContext.Request.Headers.ContainsKey(CommonConstant.PayloadHeaderName) || isUnSecure)
            {
                var payloadData = accessor.HttpContext.Request.Headers[CommonConstant.PayloadHeaderName];

                var payload = isUnSecure ? payloadData.ToString() : AesDescryptionHelper.Decrypt(payloadData).ToString();

                if (!bindingContext.ModelType.IsPrimitive && bindingContext.ModelType != typeof(decimal) && bindingContext.ModelType != typeof(string))
                {
                    bindingContext.Result = ModelBindingResult.Success(
                        JsonConvert.DeserializeObject(payload, bindingContext.ModelType));
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(
                        bindingContext.ModelType == typeof(string) ? payload : Convert.ChangeType(payload, bindingContext.ModelType));
                }
            }

            await Task.CompletedTask;
        }
    }
}
