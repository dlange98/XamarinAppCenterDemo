using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Refapp.Interfaces;
using System.Threading.Tasks;
using Refapp.Models;
using Refapp.iOS.Services;
using System.Linq;
using UIKit;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace Refapp.iOS.Services
{
    public class AuthenticationService : IAuthenticator
    {
        public async Task<AuthenticateResponse> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                var authParam = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);
                var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), authParam);

                var result = new AuthenticateResponse
                {
                    AuthHeader = authResult.CreateAuthorizationHeader(),
                    AccessToken = authResult.AccessToken,
                    AccessTokenType = authResult.AccessTokenType,
                    ExpiresOn = authResult.ExpiresOn,
                    ExtendedLifetimeToken = authResult.ExtendedLifeTimeToken,
                    IdToken = authResult.IdToken,
                    TenantId = authResult.TenantId
                };

                result.Profile.DisplayableId = authResult.UserInfo.DisplayableId;
                result.Profile.FamilyName = authResult.UserInfo.FamilyName;
                result.Profile.GivenName = authResult.UserInfo.GivenName;
                result.Profile.UniqueId = authResult.UserInfo.UniqueId;

                return result;
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Accept");
                return null;
            }
        }

        public async Task ClearToken(string authority)
        {
            await Task.Run(() =>
            {
                var authContext = new AuthenticationContext(authority);
                authContext.TokenCache.Clear();

                foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                {
                    NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                }
            });
        }
    }
}
