using System;
using System.Collections.Generic;
using System.Text;
using static EventYojana.API.BusinessLayer.Constants.CommonConstant;

namespace EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common
{
    public class AuthenticateRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AuthenticateEnum UserType { get; set; }
    }
}
