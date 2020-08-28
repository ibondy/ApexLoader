namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Link
    {
        [JsonPropertyName("link")]
        public bool link { get; set; }

        [JsonPropertyName("linkKey")]
        public string LinkKey { get; set; }

        [JsonPropertyName("linkState")]
        public int LinkState { get; set; }
    }
}