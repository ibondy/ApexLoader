namespace ApexLoader.ApexConfig
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class Alarm
    {
        [JsonPropertyName("max")]
        public double Max { get; set; }
        [JsonPropertyName("min")]
        public double Min { get; set; }
    }
}