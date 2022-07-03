using MicroservicesDemo.Queries;
using Newtonsoft.Json;
namespace MicroservicesDemo.Users.Queries.Authentication
{
    public class AuthenticateQuery : IQuery<AuthenticateResult>
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}