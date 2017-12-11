// BaseService.cs
// MyWork Mobile
//
// Created on 3/6/2017 by Scott Morgan
//
// Base class for all services.

using System;
using System.Threading.Tasks;
using Refapp.Exceptions;
using Refapp.Model.Response;
using ServiceStack;
using ServiceStack.Text;
using Xamarin.Forms;
using Refapp.Managers;

namespace Refapp.Services
{
	public abstract class BaseService
	{
 
		protected async Task<T> GetAsync<T>(string relativeUrl, string username, string password)
		{
			System.Diagnostics.Debug.WriteLine("URL:" + relativeUrl);
			try
			{
				var client = new JsonServiceClient();
				client.SetCredentials(username, password);
				client.AlwaysSendBasicAuthHeader = true;
				var response = await client.GetAsync<T>(relativeUrl);
				return response;
			}
			catch (WebServiceException webEx)
			{
				if (webEx.StatusCode == 401 || webEx.ErrorCode == "Unauthorized")
				{
					MessagingCenter.Send<BaseService>(this, Constants.UnathorizedReturnCode);
					throw new UnauthorizedException();
				}
				else
				{
					throw webEx;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Exception processing GET request for {typeof(T).Name}\n{ex.Message}");
				throw;
			}
		}

		protected async Task<T> GetAsync<T>(string relativeUrl)
		{
			System.Diagnostics.Debug.WriteLine("URL:" + relativeUrl);
			var client = new JsonServiceClient();

            // Testing
            // client.BearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjlGWERwYmZNRlQyU3ZRdVhoODQ2WVR3RUlCdyIsImtpZCI6IjlGWERwYmZNRlQyU3ZRdVhoODQ2WVR3RUlCdyJ9.eyJhdWQiOiJodHRwczovL2dyYXBoLndpbmRvd3MubmV0IiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvNDE0ZWZjMzMtNjhmZS00NTIwLTgwMmYtYWVhNDQwMTE5MmQwLyIsImlhdCI6MTQ5ODU3NTgyMiwibmJmIjoxNDk4NTc1ODIyLCJleHAiOjE0OTg1Nzk3MjIsImFjciI6IjEiLCJhaW8iOiJZMlpnWURpUTRDOVpvUHJVcE9tVlo2SnZwS2VsWTkvTHF4Ym1DMmVaQ2R4YXd1THIwQUlBIiwiYW1yIjpbInB3ZCJdLCJhcHBpZCI6ImQ2MzdiYmE2LTBmMDgtNGY5Ny1hYzZmLTYxNzBjNzMyMTk2ZSIsImFwcGlkYWNyIjoiMCIsImVfZXhwIjoyNjI4MDAsImZhbWlseV9uYW1lIjoiTW9yZ2FuIiwiZ2l2ZW5fbmFtZSI6IlNjb3R0IiwiaXBhZGRyIjoiNjMuMjUwLjg5LjMwIiwibmFtZSI6Ik1vcmdhbiwgU2NvdHQiLCJvaWQiOiI0NTRhMzdiMi0wMjM2LTQ4NjMtODUzNS1iY2Y1NTUzYTkzYzAiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtODYxNjk3OTkzLTIxNDcyODM2MC0xOTY1MDY1MjctMTk4MSIsInBsYXRmIjoiMiIsInB1aWQiOiIxMDAzM0ZGRjgwRjI4NzlGIiwic2NwIjoiVXNlci5SZWFkIiwic3ViIjoieWRpd1R6RFZPeUtFMldocmU4LXVzaHVGZThxRS1wM0JTMDRoaE1GY1hxRSIsInRpZCI6IjQxNGVmYzMzLTY4ZmUtNDUyMC04MDJmLWFlYTQ0MDExOTJkMCIsInVuaXF1ZV9uYW1lIjoiU01vcmdhbkBjYXJkaW5hbHNvbHV0aW9ucy5jb20iLCJ1cG4iOiJTTW9yZ2FuQGNhcmRpbmFsc29sdXRpb25zLmNvbSIsInV0aSI6Ik9hS0J6dGpFYVVLUWFRUGZyeGdtQUEiLCJ2ZXIiOiIxLjAifQ.Pw0kQTIzHcp6k9J3CrYgRkPKfHil3AmGphW6fjO1Ns6qtjSwbYRBO1mn96kbJyFumCEuiHk1cO8nOgxSs1yK3CobyosT5yRPHndGlSzZDFl8k9y_CnNKq_51iL6JLfrKRYyiaR-dAUCwEiWZHUXU_DUPktIgiKTr-ByieA0FJO-KXLpKQJsnjDbZxcACMUyjvfbr5y3SnOaRJbnPG9nOPuovWRiLisjDelhvXQndp5h2mwLEIWW0TdtDvKgod0ADErWVQ9tQTekUPg3k8cz9sXDtltG4OddSStMu_AMZjIZV4k_pZSqRU-Oe973AtCnxOtBhFuw2TKUqXoRInQavpA";
            client.BearerToken = AuthenticationManager.GetCurrentToken().Token;

			//var currentUser = LocalDataManager.GetUser();
			//if (currentUser != null)
			//{
			//	client.SetCredentials(currentUser.UserName, currentUser.Password);
			//	client.AlwaysSendBasicAuthHeader = true;
			//}

			try
			{
				var response = await client.GetAsync<T>(relativeUrl);
				return response;
			}
			catch (WebServiceException webEx)
			{
				if (webEx.StatusCode == 401 || webEx.ErrorCode == "Unauthorized")
				{
					MessagingCenter.Send<BaseService>(this, Constants.UnathorizedReturnCode);
					throw new UnauthorizedException();
				}
				else
				{
					throw webEx;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Exception processing POST request for {typeof(T).Name}\n{ex.ToString()}");
				System.Diagnostics.Debug.WriteLine($"URL:{relativeUrl}");
				throw;
			}
		}

		protected async Task<T> PostAsync<T>(string relativeUrl, object data)
		{
			System.Diagnostics.Debug.WriteLine("URL:" + relativeUrl);
			var client = new JsonServiceClient();
			//var currentUser = LocalDataManager.GetUser();
			//if (currentUser != null)
			//{
			//	client.SetCredentials(currentUser.UserName, currentUser.Password);
			//	client.AlwaysSendBasicAuthHeader = true;
			//}
			//else
			//{
			//	MessagingCenter.Send<BaseService>(this, Constants.UnathorizedReturnCode);
			//	throw new UnauthorizedException();
			//}

			try
			{
				var response = await client.PostAsync<T>(relativeUrl, data);
				return response;
			}
			catch (WebServiceException webEx)
			{
				if (webEx.StatusCode == 401 || webEx.ErrorCode == "Unauthorized")
				{
					MessagingCenter.Send<BaseService>(this, Constants.UnathorizedReturnCode);
					throw new UnauthorizedException();
				}
				else if (!string.IsNullOrEmpty(webEx.ResponseBody))
				{
					var svcResponse = JsonSerializer.DeserializeFromString<GeneralResponse>(webEx.ResponseBody);
					throw new Exception(svcResponse.StatusMessage);
				}
				else
				{
					throw webEx;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Exception processing POST request for {typeof(T).Name}\n{ex.Message}");
				throw;
			}
		}
	}
}
