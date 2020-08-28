namespace ApexLoader.ApexStatus
{
    using global::System.Collections.Generic;
    using global::System.Text.Json.Serialization;

    public class StatusRoot
    {
        public string ApexId { get; set; }

        [JsonPropertyName("feed")]
        public Feed Feed { get; set; }

        [JsonPropertyName("inputs")]
        public List<Input> Inputs { get; set; }

        [JsonPropertyName("link")]
        public Link Link { get; set; }

        [JsonPropertyName("modules")]
        public List<Module> Modules { get; set; }

        [JsonPropertyName("nstat")]
        public Nstat Nstat { get; set; }

        [JsonPropertyName("outputs")]
        public List<Output> Outputs { get; set; }

        [JsonPropertyName("power")]
        public Power Power { get; set; }

        [JsonPropertyName("system")]
        public System System { get; set; }
    }
}