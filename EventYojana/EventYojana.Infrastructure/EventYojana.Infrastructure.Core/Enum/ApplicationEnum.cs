using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Enum
{
    public static class ApplicationEnum
    {
        public enum ApplicationType
        {
            Admin = 1,
            Vendor = 2,
            Customer = 3
        }
        public enum ModuleActionType
        {
            Add,
            Edit,
            View,
            Delete
        }

        public enum ModuleName
        {
            VendorRequestedRegistration = 1
        }

        public enum UserRoleEnum
        {
            None = 0,
            SuperAdmin = 1,
            Admin = 2,
            Vendor = 3,
            Customer = 5
        }
    }
}
