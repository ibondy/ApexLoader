
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexLoader
{
    using System.Text.Json.Serialization;

    public class ApexLoginContext
    {
        [JsonPropertyName("login")]
        public string User { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("remember_me")]
        public bool RememberMe { get; set; }
    }


}
