using EventYojana.Infrastructure.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromEncryptedBodyAttribute : Attribute, IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext modelBinderContext)
        {
            var request = modelBinderContext.ActionContext.HttpContext.Request;
            if(request.Method != HttpMethods.Get)
            {
                bool isUnSecure = false;
                var isUnSecureHeader = request.Headers["X-isUnSecureSwagger"];
                if(!string.IsNullOrEmpty(isUnSecureHeader))
                {
                    bool.TryParse(isUnSecureHeader.ToString(), out isUnSecure);
                }

                using (var streamReader = new StreamReader(request.Body))
                {
                    var body = await streamReader.ReadToEndAsync();
                    var decryptBody = isUnSecure ? body : AesDescryptionHelper.Decrypt(body);
                    
                    if(!modelBinderContext.ModelType.IsPrimitive && modelBinderContext.ModelType != typeof(decimal) && modelBinderContext.ModelType != typeof(string))
                    {
                        modelBinderContext.Result = ModelBindingResult.Success(
                            JsonConvert.DeserializeObject(decryptBody, modelBinderContext.ModelType));
                    }
                    else
                    {
                        modelBinderContext.Result = ModelBindingResult.Success(
                            modelBinderContext.ModelType == typeof(string) ? decryptBody : Convert.ChangeType(decryptBody, modelBinderContext.ModelType));
                    }
                }
            }
        }
    }
}
