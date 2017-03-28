using Newtonsoft.Json;

namespace GrubBuddy.Models
{
    public class AuthParam
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecet { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }
    }
}
