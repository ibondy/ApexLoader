namespace ApexLoader.ApexLog
{
    using System.Text.Json.Serialization;

    public class Datum
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}