// IAccessTokenDAO.cs
// Scott Morgan
// 6/29/2017
// 2:35 PM
using System;
using Refapp.Models;

namespace Refapp.DAO
{
	public interface IAccessTokenDAO
	{
		AccessToken GetCurrentToken();
		void InsertOrUpdateToken(AccessToken token);
	}
}
