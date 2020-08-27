namespace ApexLoader.ApexConfig
{
    using System.Text.Json.Serialization;
    /// <summary>
    /// Calibaration ???
    /// </summary>
    public class Cal
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }
        [JsonPropertyName("value")]
        public int Value { get; set; }
        [JsonPropertyName("state")]
        public int State { get; set; }
    }
}