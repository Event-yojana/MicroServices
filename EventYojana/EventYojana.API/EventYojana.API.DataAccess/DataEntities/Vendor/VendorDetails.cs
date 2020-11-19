using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Vendor
{
    public class VendorDetails
    {
        public int VendorId { get; set; }
        public int LoginId { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public bool IsBranch { get; set; }
    }
}
