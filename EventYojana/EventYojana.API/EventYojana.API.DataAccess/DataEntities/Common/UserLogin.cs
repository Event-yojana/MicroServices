using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
    public class UserLogin
    {
        public int LoginId { get; set; }
        public int UserType { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsVerifiedUser { get; set; }
    }
}
