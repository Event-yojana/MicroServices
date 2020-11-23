using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Common;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.Infrastructure.Core.Models;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Commons
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);

        Task<PostResponseModel> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel);
    }
}
