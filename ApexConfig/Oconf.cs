namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;
    /// <summary>
    /// Output configuration
    /// </summary>
    public class Oconf
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonPropertyName("ctype")]
        public string Ctype { get; set; }
        [JsonPropertyName("log")]
        public bool Log { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("did")]
        public string Did { get; set; }
        [JsonPropertyName("gid")]
        public string Gid { get; set; }
        [JsonPropertyName("gtype")]
        public string Gtype { get; set; }
        [JsonPropertyName("ID")]
        public string Id { get; set; }
        [JsonPropertyName("prog")]
        public string Prog { get; set; }
        [JsonPropertyName("extra")]
        public Extra Extra { get; set; }
    }
}