using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;
using Refapp.Configuration;
using Refapp.DAO;
using Refapp.Managers;
using Refapp.Models;
using Xamarin.Forms;

using Microsoft.AppCenter.Analytics;

namespace Refapp.Services
{
    public class CloudDataStore : IDataStore<Item>
    {
        HttpClient client;
        IEnumerable<Item> items;

        protected AccessTokenDAO TokenDAO;

        public CloudDataStore()
        {
            var fileManager = DependencyService.Get<IFileManager>();
            TokenDAO = new AccessTokenDAO(fileManager);

            client = new HttpClient();
            client.BaseAddress = new Uri($"{Settings.AppServiceURL}/");
 
            items = new List<Item>();
        }

        public async Task UpdateAuthTokenInHeaderAsync()
        {

            var token = TokenDAO.GetCurrentToken();

            if (token == null || token.TokenExpired){
                token = await LoginAsync();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            await UpdateAuthTokenInHeaderAsync();
            System.Diagnostics.Debug.Print("after the await in GetItemAsync");
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            await UpdateAuthTokenInHeaderAsync();
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            await UpdateAuthTokenInHeaderAsync();

            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            await UpdateAuthTokenInHeaderAsync();

            if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            await UpdateAuthTokenInHeaderAsync();

            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }


        private async Task<AccessToken> LoginAsync()
        {
            // Present login dialog from our OS dependent authentication Logic
            var response = await DependencyService.Get<IAuthenticator>().Authenticate(Settings.TenantId, Settings.ResourceId, Settings.ClientId, Settings.ReturnUrl);
            var tokenData = new AccessToken
            {
                Token = response.AccessToken,
                TokenType = response.AccessTokenType,
                ExpiresOn = response.ExpiresOn
            };

            // track a succesfull login
            Analytics.TrackEvent("Login completed");

            // persist our access token to be used for server requests
            TokenDAO.InsertOrUpdateToken(tokenData);
            return tokenData;
        }

        public bool IsLoginNeeded()
        {
            var token = TokenDAO.GetCurrentToken();

            if (token == null || token.TokenExpired)
            {
                return true;
            }

            return false;
        }

       
    }
}
