using System.Text.Json.Serialization;

namespace ApexLoader.ApexOlog
{
    public class Record
    {
        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}