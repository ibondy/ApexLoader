namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Extra2
    {
        [JsonPropertyName("auto")]
        public List<bool> Auto { get; set; }

        [JsonPropertyName("cal")]
        public bool? Cal { get; set; }

        [JsonPropertyName("calConst")]
        public List<double> CalConst { get; set; }

        [JsonPropertyName("errorMask")]
        public int? ErrorMask { get; set; }

        [JsonPropertyName("flowMode")]
        public string FlowMode { get; set; }

        [JsonPropertyName("homeSet")]
        public bool? HomeSet { get; set; }

        [JsonPropertyName("mode")]
        public List<string> Mode { get; set; }

        [JsonPropertyName("newRegent")]
        public List<bool> NewReagent { get; set; }

        [JsonPropertyName("prime")]
        public List<bool> Prime { get; set; }

        [JsonPropertyName("primeBlank")]
        public bool? PrimeBlank { get; set; }

        [JsonPropertyName("reset")]
        public List<bool> Reset { get; set; }

        [JsonPropertyName("resertDefaults")]
        public bool? ResetDefaults { get; set; }

        [JsonPropertyName("sampleChange")]
        public bool? SampleChange { get; set; }

        [JsonPropertyName("swapAddr")]
        public int SwapAddr { get; set; }

        [JsonPropertyName("targetAlk")]
        public double? TargetAlk { get; set; }

        [JsonPropertyName("targetCa")]
        public int? TargetCa { get; set; }

        [JsonPropertyName("targetMg")]
        public int? TargetMg { get; set; }

        [JsonPropertyName("wasteSize")]
        public double? WasteSize { get; set; }
    }
}