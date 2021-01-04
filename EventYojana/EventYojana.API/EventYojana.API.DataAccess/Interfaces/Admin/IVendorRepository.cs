using EventYojana.API.DataAccess.DataEntities.Vendor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Interfaces.Admin
{
    public interface IVendorRepository
    {
        Task<IEnumerable<VendorDetails>> GetRegisteredVendorList();
        Task<RegisterVendorResponse> ConfirmRegistration(int vendorId, string password, string passwordSalt);
    }
}
