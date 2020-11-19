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
            Admin = 1,
            Vendor = 2,
            Customer = 3
        }
    }
}
