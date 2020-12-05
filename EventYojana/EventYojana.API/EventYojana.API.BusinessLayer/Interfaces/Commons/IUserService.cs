using EventYojana.Infrastructure.Core.Models;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Interfaces.Commons
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);

    }
}
