namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ConfigRoot
    {
        public string ApexId { get; set; }

        [JsonPropertyName("cal")]
        public Cal Cal { get; set; }

        [JsonPropertyName("clock")]
        public Clock Clock { get; set; }

        [JsonPropertyName("dconf")]
        public List<Dconf> Dconf { get; set; }

        [JsonPropertyName("iconf")]
        public List<Iconf> Iconf { get; set; }

        [JsonPropertyName("mconf")]
        public List<Mconf> Mconf { get; set; }

        [JsonPropertyName("misc")]
        public Misc Misc { get; set; }

        [JsonPropertyName("nconf")]
        public Nconf Nconf { get; set; }

        [JsonPropertyName("oconf")]
        public List<Oconf> Oconf { get; set; }

        [JsonPropertyName("pconf")]
        public List<Pconf> Pconf { get; set; }

        [JsonPropertyName("season")]
        public Season Season { get; set; }
    }
}