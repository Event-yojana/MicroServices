using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess
{
    public static class StoreProcedureSchemas
    {
        private static string DboSchema = "dbo";
        private static string VendorSchema = "vendor";

        public static string usp_RegisterVendor = string.Format("{0}.usp_RegisterVendor", VendorSchema);
        public static string usp_GetVendorsDetails = string.Format("{0}.usp_GetVendorsDetails", VendorSchema);
        public static string usp_ConfirmVendorRegistration = string.Format("{0}.usp_ConfirmVendorRegistration", VendorSchema);
    }
}
