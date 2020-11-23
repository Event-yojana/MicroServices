using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Interfaces.Common
{
    public interface IAuthenticateRepository
    {
        Task<UserLogin> GetUserDetails(Expression<Func<UserLogin, bool>> filter);
        Task<bool> IsUserDetails(Expression<Func<UserLogin, bool>> filter);
        Task<bool> RegisterVendor(RegisterVendor registerUser);
    }
}
