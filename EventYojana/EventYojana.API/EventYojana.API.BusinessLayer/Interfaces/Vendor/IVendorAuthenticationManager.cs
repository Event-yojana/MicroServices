using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Vendor;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Vendor
{
    public interface IVendorAuthenticationManager
    {
        Task<PostResponseModel> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel);
    }
}
