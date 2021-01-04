using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Vendor
{
    public class RegisterVendorRequestModel
    {
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string VendorMobile { get; set; }
        public string VendorLandline { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
    }
}
