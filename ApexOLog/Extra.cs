using System.Text.Json.Serialization;

namespace ApexLoader.ApexOlog
{
    public class Extra
    {
        [JsonPropertyName("sdver")]
        public string Sdver { get; set; }
    }
}