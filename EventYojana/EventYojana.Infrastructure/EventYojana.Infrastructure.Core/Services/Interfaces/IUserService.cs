using EventYojana.Infrastructure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest);
        IList<AuthenticateRequest> GetUserDetails();
    }
}
