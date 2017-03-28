using System;
using System.Threading.Tasks;
using GrubBuddy.Api;
using GrubBuddy.Models;

namespace GrubBuddy.DataAccess
{
    public interface IAuth0Repository
    {
        Task<string> GetAccessToken();
    }
    public class Auth0Repository : IAuth0Repository
    {
        private readonly IAuth0Api _authApi;
        private AccessToken _currentAccessToken;
        private DateTime _accessTokenExp;
        private bool IsExpired => DateTime.Compare(DateTime.Now, _accessTokenExp) > 0;

        public Auth0Repository(IAuth0Api authApi)
        {
            _authApi = authApi;
        }
        public async Task<string> GetAccessToken()
        {
            if (!string.IsNullOrEmpty(_currentAccessToken?.Token) && !IsExpired)
                return _currentAccessToken.Token;

            _currentAccessToken = await _authApi.GetAccessToken();
            _accessTokenExp = DateTime.Now.AddSeconds(_currentAccessToken?.ExpiresIn ?? 0);

            return _currentAccessToken?.Token;
        }
    }
}
