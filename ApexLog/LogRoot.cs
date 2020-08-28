namespace ApexLoader.ApexLog
{
    using System.Text.Json.Serialization;

    public class LogRoot
    {
        public string ApexId { get; set; }

        [JsonPropertyName("ilog")]
        public Ilog Ilog { get; set; }
    }
}