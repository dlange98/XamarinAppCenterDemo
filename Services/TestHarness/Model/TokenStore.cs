using System;
using System.Collections.Generic;
using System.Text;
using TestHarness.Model;

namespace TestHarness
{
    public class TokenStore
    {
        private Model.AccessToken _Token;

        public Model.AccessToken CurrentToken
        {
            get { return _Token; }
        }

        public void UpdateToken(AccessToken NewToken)
        {
            _Token = NewToken;
        }
        public bool IsCurrent()
        {
            if (_Token == null) return false;
            return !_Token.TokenExpired;
        }
    }
}
