namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Input configuration
    /// </summary>
    public class Iconf
    {
        [JsonPropertyName("alarm")]
        public Alarm Alarm { get; set; }

        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("enable")]
        public bool Enable { get; set; }

        [JsonPropertyName("extra")]
        public Extra3 Extra { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}