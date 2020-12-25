using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.Infrastructure.Core.Models;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Commons
{
    public interface IUserService
    {
        Task<GetResponseModel> Authenticate(string UserName, string Password, int[] UserType);

    }
}
