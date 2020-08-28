namespace ApexLoader.ApexLog
{
    using System.Text.Json.Serialization;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Extra
    {
        [JsonPropertyName("sdver")]
        public string Sdver { get; set; }
    }
}