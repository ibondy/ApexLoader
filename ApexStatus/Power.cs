namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Power    
    {
        [JsonPropertyName("failed")]
        public int Failed { get; set; }
        [JsonPropertyName("restored")]
        public int Restored { get; set; } 
    }
}