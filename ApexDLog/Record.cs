﻿using System.Text.Json.Serialization;

namespace ApexLoader.ApexDLog
{
    public class Record
    {
        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}