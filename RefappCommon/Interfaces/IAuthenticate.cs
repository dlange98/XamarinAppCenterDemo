using System;
using System.Threading.Tasks;
using Refapp.Models;

namespace Refapp.Interfaces
{
    public interface IAuthenticator
    {
        Task<AuthenticateResponse> Authenticate(string authority, string resource, string clientId, string returnUri);
        Task ClearToken(string authority);
    }
}
