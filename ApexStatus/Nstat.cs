namespace ApexLoader.ApexStatus
{
    #region using

    using global::System.Collections.Generic;
    using global::System.Text.Json.Serialization;

    #endregion

    /// <summary>
    ///     Network status
    /// </summary>
    public class Nstat
    {
        [JsonPropertyName("dhcp")]
        public bool Dhcp { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("ipaddr")]
        public string Ipaddr { get; set; }

        [JsonPropertyName("netmask")]
        public string Netmask { get; set; }

        [JsonPropertyName("gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("dns")]
        public List<string> Dns { get; set; }

        [JsonPropertyName("httpPort")]
        public int HttpPort { get; set; }

        [JsonPropertyName("fusionEnable")]
        public bool FusionEnable { get; set; }

        [JsonPropertyName("quality")]
        public int Quality { get; set; }

        [JsonPropertyName("strenght")]
        public int Strength { get; set; }

        [JsonPropertyName("link")]
        public bool Link { get; set; }

        [JsonPropertyName("wifiAPLock")]
        public bool WifiApLock { get; set; }

        [JsonPropertyName("wifiEnable")]
        public bool WifiEnable { get; set; }

        [JsonPropertyName("wifiPassword")]
        public string WifiApPassword { get; set; }

        [JsonPropertyName("ssid")]
        public string Ssid { get; set; }

        [JsonPropertyName("wifiAP")]
        public bool WifiAp { get; set; }

        [JsonPropertyName("emailPassword")]
        public string EmailPassword { get; set; }

        [JsonPropertyName("updateFirmware")]
        public bool UpdateFirmware { get; set; }

        [JsonPropertyName("latestFirmware")]
        public string LatestFirmware { get; set; }
    }
}