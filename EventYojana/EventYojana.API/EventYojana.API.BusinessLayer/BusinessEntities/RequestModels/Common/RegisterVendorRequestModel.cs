using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common
{
    public class RegisterVendorRequestModel
    {
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
