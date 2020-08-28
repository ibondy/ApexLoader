namespace ApexLoader.ApexLog
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Record
    {
        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }
    }
}