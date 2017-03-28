using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GrubBuddy.Models;
using MongoDB.Bson;
using Rest;

namespace GrubBuddy.Api
{
    public interface IAuth0Api
    {
        Task<AccessToken> GetAccessToken();
    }

    public class Auth0Api : IAuth0Api
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;
        private readonly string _tokenUrl;

        public Auth0Api(string clientId, string clientSecret, string audience, string tokenUrl)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _audience = audience;
            _tokenUrl = tokenUrl;
        }

       public async Task<AccessToken> GetAccessToken()
        {
            var client = new HttpClient();
            var serializer = new JsonContentSerializer();
            var param = new AuthParam
            {
                GrantType = "client_credentials",
                ClientId = _clientId,
                ClientSecet = _clientSecret,
                Audience = _audience
            };

            var rm = new HttpRequestMessage
            {
                RequestUri = new Uri(_tokenUrl),
                Content = new StringContent(serializer.Serialize(param, false), Encoding.UTF8, "application/json")

            };
            var response = await client.PostAsync(rm.RequestUri, rm.Content);
            var accessToken = serializer.Deserialize<AccessToken>(response.Content.ReadAsStringAsync().Result);

            return accessToken;
        }
    }
}
