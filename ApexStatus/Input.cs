namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Input    
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } 
        [JsonPropertyName("value")]
        public double Value { get; set; } 
    }
}