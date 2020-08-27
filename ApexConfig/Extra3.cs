namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;

    public class Extra3
    {
        [JsonPropertyName("scale")]
        public string Scale { get; set; }
        [JsonPropertyName("offset")]
        public string Offset { get; set; }
        [JsonPropertyName("range")]
        public string Range { get; set; }
        [JsonPropertyName("comp")]
        public object Comp { get; set; }
    }
}