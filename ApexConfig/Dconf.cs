namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Display configuration
    /// </summary>
    public class Dconf
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("lineEnables")]
        public List<bool> LineEnables { get; set; }
        [JsonPropertyName("probes")]
        public List<string> Probes { get; set; }
        [JsonPropertyName("outputs")]
        public List<string> Outputs { get; set; }
    }
}