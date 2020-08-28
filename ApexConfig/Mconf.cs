namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Module configuration
    /// </summary>
    public class Mconf
    {
        [JsonPropertyName("abaddr")]
        public int Abaddr { get; set; }

        [JsonPropertyName("extra")]
        public Extra2 Extra { get; set; }

        [JsonPropertyName("hwtype")]
        public string Hwtype { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("update")]
        public bool Update { get; set; }

        [JsonPropertyName("updateStats")]
        public int UpdateStat { get; set; }
    }
}