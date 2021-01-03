using EventYojana.Infrastructure.Core.Enum;
using EventYojana.Infrastructure.Core.Interfaces;
using EventYojana.Infrastructure.Core.Models;
using EventYojana.Infrastructure.Core.Result;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Attributes
{
    public class AccessAttribute : ActionFilterAttribute
    {
        private IRequestContext _requestContext { get; set; }
        protected ApplicationEnum.ModuleName ModuleName { get; set; }
        protected ApplicationEnum.ModuleActionType ActionType { get; set; }
        public AccessAttribute(IRequestContext requestContext,
            ApplicationEnum.ModuleName moduleName,
            ApplicationEnum.ModuleActionType actionType)
        {
            _requestContext = requestContext;
            ModuleName = moduleName;
            ActionType = actionType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context == null)
            {
                return;
            }
            else
            {
                if(_requestContext.LogOnUserDetails.UserModule.Count == 0 || _requestContext.LogOnUserDetails.UserId == null || _requestContext.LogOnUserDetails.UserId == 0)
                {
                    context.Result = new HttpForbiddenResult();
                    return;
                }

                UserModule userModule = _requestContext.LogOnUserDetails.UserModule.FirstOrDefault(x => x.ModuleId == (int)ModuleName);

                if(!IsAccessModule(ActionType, userModule))
                {
                    context.Result = new HttpForbiddenResult();
                    return;
                }
            }
            await next();
        }

        private bool IsAccessModule(ApplicationEnum.ModuleActionType actionType, UserModule userModule)
        {
            switch (actionType)
            {
                case ApplicationEnum.ModuleActionType.Add:
                    return userModule.IsAdd;
                case ApplicationEnum.ModuleActionType.Edit:
                    return userModule.IsEdit;
                case ApplicationEnum.ModuleActionType.Delete:
                    return userModule.IsDelete;
                case ApplicationEnum.ModuleActionType.View:
                    return userModule.IsView;
                default:
                    return false;
            }
        }
    }
}
