using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Vendor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Admin
{
    public interface IVendorManager
    {
        Task<List<RegisteredVendorsResponseModel>> GetRegisteredVendorsList();
        Task<PostResponseModel> ConfirmRegistration(int vendorId);
    }
}
