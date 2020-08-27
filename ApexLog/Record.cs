namespace ApexLoader.ApexLog
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Record    
    {
        [JsonPropertyName("date")]
        public int Date { get; set; } 
        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; } 
    }
}