namespace ApexLoader.ApexStatus
{
    using global::System.Collections.Generic;
    using global::System.Text.Json.Serialization;

    public class Extra2
    {
        [JsonPropertyName("errorCode")]
        public int? ErrorCode { get; set; }

        [JsonPropertyName("errorMask")]
        public int? ErrorMask { get; set; }

        [JsonPropertyName("lastCal")]
        public int? LastCal { get; set; }

        [JsonPropertyName("levels")]
        public List<double> Levels { get; set; }

        [JsonPropertyName("restTime")]
        public List<int> ResetTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("temp")]
        public int? Temp { get; set; }
    }
}