using System.Text.Json.Serialization;

namespace ApexLoader.ApexTLog
{
    public class Extra
    {
        [JsonPropertyName("sdver")]
        public string Sdver { get; set; }
    }
}