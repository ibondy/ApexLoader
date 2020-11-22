using System.Text.Json.Serialization;

namespace ApexLoader.ApexDLog
{
    public class Extra
    {
        [JsonPropertyName("sdver")]
        public string Sdver { get; set; }
    }
}