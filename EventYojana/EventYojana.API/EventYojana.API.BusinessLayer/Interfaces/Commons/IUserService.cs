using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Vendor;
using EventYojana.Infrastructure.Core.Models;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Commons
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);

        Task<bool> RegisterBranch(VendorDetailsRequestModel vendorDetailsRequestModel);
    }
}
