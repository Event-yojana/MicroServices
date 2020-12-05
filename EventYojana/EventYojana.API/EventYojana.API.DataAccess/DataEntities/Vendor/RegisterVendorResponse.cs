using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Vendor
{
    public class RegisterVendorResponse
    {
        public bool IsUserExists { get; set; }
        public bool Success { get; set; }
        public int VendorId { get; set; }
    }
}
