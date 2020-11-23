using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common;
using EventYojana.Infrastructure.Core.Models;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Commons
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);

        Task<bool> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel);
    }
}
