namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class System
    {
        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("extra")]
        public Extra Extra { get; set; }

        [JsonPropertyName("hardware")]
        public string Hardware { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("serial")]
        public string Serial { get; set; }

        [JsonPropertyName("software")]
        public string Software { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}