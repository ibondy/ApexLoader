namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Feed
    {
        [JsonPropertyName("active")]
        public int Active { get; set; }

        [JsonPropertyName("name")]
        public int Name { get; set; }
    }
}