using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Enum
{
    public static class SecurityEnum
    {
        public enum ActionType
        {
            Add,
            Edit,
            View,
            Delete
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
