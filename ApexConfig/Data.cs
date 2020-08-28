namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;

    public class Data
    {
        [JsonPropertyName("div10")]
        public string Div10 { get; set; }

        [JsonPropertyName("max")]
        public int Max { get; set; }

        [JsonPropertyName("min")]
        public int Min { get; set; }

        [JsonPropertyName("offtime0")]
        public int OffTime0 { get; set; }

        [JsonPropertyName("offTime1")]
        public int OffTime1 { get; set; }

        [JsonPropertyName("onTime")]
        public int OnTime { get; set; }

        [JsonPropertyName("rampTime")]
        public int? RampTime { get; set; }

        [JsonPropertyName("sync")]
        public string Sync { get; set; }
    }
}