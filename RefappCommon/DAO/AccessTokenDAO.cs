// AccesTokenDAO.cs
// Scott Morgan
// 6/29/2017
// 2:37 PM
using System;
using Refapp.Managers;
using Refapp.Models;

namespace Refapp.DAO
{
    public class AccessTokenDAO : BaseDAO, IAccessTokenDAO
	{
		public AccessTokenDAO(IFileManager fileManager) : base(fileManager)
		{
		}

		public AccessToken GetCurrentToken()
		{
			using (var db = getDBConnection())
			{
				try
				{
					var tokens = db.Query<AccessToken>("select * from AccessToken");
					if (tokens.Count > 0)
					{
						return tokens [0];
					}
					return null;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"Exception Getting the currrent Access Token {ex.Message}");
					throw;
				}
			}
		}


        public void DeleteToken()
        {
            using (var db = getDBConnection())
            {
                try
                {
                    var tokens = db.Query<AccessToken>("delete from AccessToken");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception deleting the currrent Access Token {ex.Message}");
                    throw;
                }
            }
        }

		public void InsertOrUpdateToken(AccessToken token)
		{
			using (var db = getDBConnection())
			{
				try
				{
					db.BeginTransaction();
					var userQuery = "select * from AccessToken";
					var tokens = db.Query<AccessToken>(userQuery);
					if (tokens.Count > 0)
					{
						var oldToken = tokens[0];
						oldToken.ExpiresOn = token.ExpiresOn;
						oldToken.Token = token.Token;
						oldToken.TokenType = token.TokenType;
						db.Update(oldToken);
					}
					else
					{
						db.Insert(token);
					}
					db.Commit();
				}
				catch (Exception ex)
				{
					db.Rollback();
					System.Diagnostics.Debug.WriteLine($"Exception Updating the currrent Access Token {ex.Message}");
					throw;
				}
			}
		}
	}
}
