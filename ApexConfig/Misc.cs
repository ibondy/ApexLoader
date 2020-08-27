﻿namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Misc
    {
        [JsonPropertyName("reboot")]
        public bool Reboot { get; set; }
        [JsonPropertyName("almSound")]
        public string AlmSound { get; set; }
        [JsonPropertyName("wrnSound")]
        public string WrnSound { get; set; }
        [JsonPropertyName("logInterval")]
        public int LogInterval { get; set; }
        [JsonPropertyName("feedInterval")]
        public List<int> FeedInterval { get; set; }
        [JsonPropertyName("pwrMon")]
        public bool PwrMon { get; set; }
    }
}