namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Display configuration
    /// </summary>
    public class Dconf
    {
        [JsonPropertyName("lineEnables")]
        public List<bool> LineEnables { get; set; }

        [JsonPropertyName("outputs")]
        public List<string> Outputs { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("probes")]
        public List<string> Probes { get; set; }
    }
}