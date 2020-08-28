namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Profile configuration
    /// </summary>
    public class Pconf
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}