using System;
using System.Collections.Generic;
using System.Text;
using static EventYojana.Infrastructure.Core.Enum.ApplicationEnum;

namespace EventYojana.Infrastructure.Core.Models
{
    public class UserSettings
    {
        public int? UserId { get; set; }
        public UserRoleEnum? UserRole { get; set; }
        public List<UserModule> UserModule { get; set; }
    }

    public class UserModule
    {
        public int ModuleId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public int ApplicationId { get; set; }
    }
}
