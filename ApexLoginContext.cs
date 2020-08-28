namespace ApexLoader
{
    using System.Text.Json.Serialization;

    public class ApexLoginContext
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("remember_me")]
        public bool RememberMe { get; set; }

        [JsonPropertyName("login")]
        public string User { get; set; }
    }
}