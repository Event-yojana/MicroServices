using EventYojana.API.DataAccess.DataEntities.Vendor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Interfaces.Vendor
{
    public interface IVendorAuthenticationRepository
    {
        Task<RegisterVendorResponse> RegisterVendor(RegisterVendor registerVenderModel);

    }
}
