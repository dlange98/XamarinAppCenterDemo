using System;
using System.Threading.Tasks;
using Android.App;
using Refapp.Droid.Services;
using Xamarin.Forms;
using System.Linq;
using Refapp.Models;
using Refapp.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateService))]

namespace Refapp.Droid.Services
{
    public class AuthenticateService : IAuthenticator
    {
        public async Task<AuthenticateResponse> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                var authParam = new PlatformParameters((Activity)Forms.Context);
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

                result.Profile.UniqueId = authResult.UserInfo.UniqueId;
                result.Profile.GivenName = authResult.UserInfo.GivenName;
                result.Profile.FamilyName = authResult.UserInfo.FamilyName;
                result.Profile.DisplayableId = authResult.UserInfo.DisplayableId;

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

                Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            });
        }
    }
}
