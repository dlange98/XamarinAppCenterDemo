// AccessTokens.cs
// Scott Morgan
// 6/29/2017
// 2:24 PM
using System;
using SQLite.Net.Attributes;

namespace Refapp.Models
{
	[Table("AccessToken")]
	public class AccessToken
	{
		[PrimaryKey, AutoIncrement]
		public Int64 Id { get; set; }

		public string Token { get; set; }

		public string TokenType { get; set; }

		public DateTimeOffset ExpiresOn { get; set; }

		public bool TokenExpired
		{
			get
			{
				return DateTimeOffset.Now.CompareTo(ExpiresOn) > 0;
			}
		}

		public AccessToken()
		{
		}

       
    }
}
