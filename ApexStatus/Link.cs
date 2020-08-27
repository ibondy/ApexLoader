namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Link    
    {
        [JsonPropertyName("linkState")]
        public int LinkState { get; set; }
        [JsonPropertyName("linkKey")]
        public string LinkKey { get; set; } 
        [JsonPropertyName("link")]
        public bool link { get; set; } 
    }
}