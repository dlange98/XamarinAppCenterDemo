using System;
using System.Collections.Generic;
using System.Text;

namespace TestHarness.Model
{
    public class AccessToken
    {
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
