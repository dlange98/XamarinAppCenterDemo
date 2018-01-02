using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestHarness.Model;
using TestHarness.Models;

namespace TestHarness
{
    internal class Program
    {
        private static string authority = "https://login.windows.net/414efc33-68fe-4520-802f-aea4401192d0";//<=specific to Cardinal.  substitute your Tenant Id
        public static string ResourceId = "1c96f60e-5d95-47b0-b113-a1837b99f623"; //<= Specific for the App Registration; OAuthBackend Application ID
        public static string ClientId = "8d08c14d-1fab-4cbc-8766-b02816e8589a";//<= Specific for the App Registration; OAuthMobileApp Application ID
        public static string ReturnUrl = $"https://localhost";
        public static TokenStore _TS = new TokenStore();

        private static void Main(string[] args)
        {
            Console.WriteLine("processing Get request tests to API");
            ClearToken(authority).Wait();
            var token = GetToken().Result;
            Item itemobj = new Item { Id = Guid.NewGuid().ToString(), Text = $"{DateTime.Now.ToString()} :: API Post from Console App", Description = "This is a post from the console App proving I can create an access token from the console" };
            Item itemn =  PostItem(token,itemobj).Result;
            Console.WriteLine($"Created Item ID {itemn.Id}");
            string itemsreply = GetItems(token);
            List<Item> items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(itemsreply);
            foreach (var item in items)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Text);
                Console.WriteLine(item.Description);
                Console.WriteLine("_______________________");
            }
            
            Console.ReadLine();
        }

        private static string GetItems(AccessToken token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core");

            return client.GetStringAsync("https://oauthbackend.azurewebsites.net/api/Item").Result;
        }
        private static async Task<Item> PostItem(AccessToken token, Item item)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Core");
            HttpContent newItem = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item));
            var response = await client.PostAsync("https://oauthbackend.azurewebsites.net/api/Item", newItem);
            var reply = await response.Content.ReadAsStringAsync();
            Item replyItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(reply);
            return replyItem;
        }
        private static async Task<AccessToken> GetToken()
        {
            try
            {
                if (!_TS.IsCurrent())
                {
                    var token = await GetNewToken();
                    _TS.UpdateToken(token);
                }
                return _TS.CurrentToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static async Task<AccessToken> GetNewToken()
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                if (authContext.TokenCache.ReadItems().Any())
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                var authParam = new PlatformParameters(PromptBehavior.Always);

                var authResult = await authContext.AcquireTokenAsync(ResourceId, ClientId, new Uri(ReturnUrl), authParam);
                var result = new
                {
                    AuthHeader = authResult.CreateAuthorizationHeader(),
                    AccessToken = authResult.AccessToken,
                    AccessTokenType = authResult.AccessTokenType,
                    ExpiresOn = authResult.ExpiresOn,
                    ExtendedLifetimeToken = authResult.ExtendedLifeTimeToken,
                    IdToken = authResult.IdToken,
                    TenantId = authResult.TenantId
                };
                var tokenData = new AccessToken
                {
                    Token = result.AccessToken,
                    TokenType = result.AccessTokenType,
                    ExpiresOn = result.ExpiresOn
                };
                return tokenData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Clears the token form the cookie storage
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="authority">Authority.</param>
        public static async Task ClearToken(string authority)
        {
            await Task.Run(() =>
            {
                var authContext = new AuthenticationContext(authority);
                authContext.TokenCache.Clear();

                //foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                //{
                //    NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                //}
            });
        }
    }
}