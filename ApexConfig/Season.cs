namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Season
    {
        [JsonPropertyName("moonrise")]
        public List<string> Moonrise { get; set; }

        [JsonPropertyName("moonset")]
        public List<string> Moonset { get; set; }

        [JsonPropertyName("newmoon")]
        public List<int> Newmoon { get; set; }

        [JsonPropertyName("sunrise")]
        public List<string> Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public List<string> Sunset { get; set; }

        [JsonPropertyName("temp")]
        public List<double> Temp { get; set; }
    }
}