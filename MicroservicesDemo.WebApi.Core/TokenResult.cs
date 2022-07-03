using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.WebApi
{
    public class TokenResult
    {
        [JsonProperty("access_token")]
        [Required]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        [Required]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        [Required]
        public string TokenType => "bearer";
    }
}
