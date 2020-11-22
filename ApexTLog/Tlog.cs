﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexLoader.ApexTLog
{
    public class Tlog
    {
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("software")]
        public string Software { get; set; }

        [JsonPropertyName("hardware")]
        public string Hardware { get; set; }

        [JsonPropertyName("serial")]
        public string Serial { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("extra")]
        public Extra Extra { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("record")]
        public List<Record> Record { get; set; }
    }
}