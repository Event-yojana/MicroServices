using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Middlewares
{
    public sealed class CheckAppKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CheckAppKeyMiddleware> _logging;
        private readonly AppKeyConfig _appKeyConfig;

        public CheckAppKeyMiddleware(RequestDelegate next,ILogger<CheckAppKeyMiddleware> logging, IOptionsMonitor<AppKeyConfig> appKeyConfigAccessor)
        {
            try {
                _logging = logging ?? throw new ArgumentNullException(nameof(logging));
                _next = next ?? throw new ArgumentNullException(nameof(next));
                if (appKeyConfigAccessor == null)
                {
                    throw new ArgumentNullException(nameof(appKeyConfigAccessor));
                }
                _appKeyConfig = appKeyConfigAccessor.CurrentValue;
                appKeyConfigAccessor.OnChange(a =>
                {
                    if (a.HeaderName == null || a.AppKeys == null)
                    {
                        _logging.LogError("Changed AppKey configuration at runtime with invalid values");
                    }
                    _appKeyConfig.HeaderName = a.HeaderName;
                    _appKeyConfig.AppKeys = a.AppKeys;
                });

                CheckConfiguration();
            }
            catch(Exception ex)
            {

            }
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(_appKeyConfig.HeaderName, out StringValues values) || !ContainsKey(values))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _logging.LogError($"Missing or incorrect AppKey config in Request HeaderName ' {_appKeyConfig.HeaderName}'" + $"with values: '{values.ToString()}'");
                return;
            }
            await _next(context);
        }

        private bool ContainsKey(StringValues values)
        {
            foreach(AppKey appKey in _appKeyConfig.AppKeys)
            {
                if(values.Contains(appKey.KeyValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckConfiguration()
        {
            if(_appKeyConfig == null)
            {
                throw new ConfigurationNotFoundException(nameof(_appKeyConfig));
            }
            else if(string.IsNullOrEmpty(_appKeyConfig.HeaderName))
            {
                throw new ConfigurationNotFoundException(nameof(_appKeyConfig) + ":" + nameof(_appKeyConfig.HeaderName));
            }
            else if(_appKeyConfig.AppKeys == null)
            {
                throw new ConfigurationNotFoundException(nameof(_appKeyConfig) + ":" + nameof(_appKeyConfig.AppKeys));
            }
        }
    }
}
