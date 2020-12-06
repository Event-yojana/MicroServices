using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Vendor;
using EventYojana.API.BusinessLayer.Interfaces.Admin;
using EventYojana.API.DataAccess.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Managers.Admin
{
    public class VendorManager : IVendorManager
    {
        public readonly IVendorRepository _vendorRepository;

        public VendorManager(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<List<RegisteredVendorsResponseModel>> GetRegisteredVendorsList()
        {
            var vendorDetails = await _vendorRepository.GetRegisteredVendorList();

            return vendorDetails.ToList().Select(x => new RegisteredVendorsResponseModel() { 
                VendorId = x.VendorId,
                VendorName = x.VendorName,
                VendorEmail = x.VendorEmail,
                Mobile = x.Mobile,
                Landline = x.Landline,
                Address = x.FullAddress
            }).ToList();
        }
    }
}
