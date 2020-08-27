namespace ApexLoader.ApexStatus
{
    using global::System.Text.Json.Serialization;

    public class Module    
    {
        [JsonPropertyName("abaddr")]
        public int Abaddr { get; set; }
        [JsonPropertyName("hwtype")]
        public string Hwtype { get; set; }
        [JsonPropertyName("hwrev")]
        public int Hwrev { get; set; }
        [JsonPropertyName("swrev")]
        public int Swrev { get; set; }
        [JsonPropertyName("swstat")]
        public string Swstat { get; set; }
        [JsonPropertyName("pcount")]
        public int Pcount { get; set; }
        [JsonPropertyName("pgood")]
        public int Pgood { get; set; } 
        [JsonPropertyName("perror")]
        public int Perror { get; set; } 
        [JsonPropertyName("reatt")]
        public int Reatt { get; set; } 
        [JsonPropertyName("inact")]
        public int Inact { get; set; } 
        [JsonPropertyName("boot")]
        public bool Boot { get; set; } 
        [JsonPropertyName("present")]
        public bool Present { get; set; }
        [JsonPropertyName("extra")]
        public Extra2 Extra { get; set; } 
    }
}