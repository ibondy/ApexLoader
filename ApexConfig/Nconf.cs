namespace ApexLoader.ApexConfig
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Network configuration
    /// </summary>

    public class Nconf
    {
        [JsonPropertyName("defaultAuth")]
        public bool DefaultAuth { get; set; }

        [JsonPropertyName("dhcp")]
        public bool Dhcp { get; set; }

        [JsonPropertyName("dns")]
        public List<string> Dns { get; set; }

        [JsonPropertyName("emailAuth")]
        public bool EmailAuth { get; set; }

        [JsonPropertyName("emailEnable")]
        public bool EmailEnable { get; set; }

        [JsonPropertyName("emailFrom")]
        public string EmailFrom { get; set; }

        [JsonPropertyName("emailPassword")]
        public string EmailPassword { get; set; }

        [JsonPropertyName("emailTo")]
        public string EmailTo { get; set; }

        [JsonPropertyName("emailUser")]
        public string EmailUser { get; set; }

        [JsonPropertyName("fusionEnable")]
        public bool FusionEnable { get; set; }

        [JsonPropertyName("gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("httpPort")]
        public int HttpPort { get; set; }

        [JsonPropertyName("apaddr")]
        public string Ipaddr { get; set; }

        [JsonPropertyName("latestFirmware")]
        public string LatestFirmware { get; set; }

        [JsonPropertyName("netmask")]
        public string Netmask { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("reEmail")]
        public int ReEmail { get; set; }

        [JsonPropertyName("smtpPort")]
        public int SmtpPort { get; set; }

        [JsonPropertyName("smtpServer")]
        public string SmtpServer { get; set; }

        [JsonPropertyName("ssid")]
        public string Ssid { get; set; }

        [JsonPropertyName("updateFirmware")]
        public bool UpdateFirmware { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("wifiAP")]
        public bool WifiAp { get; set; }

        [JsonPropertyName("wifiAPLock")]
        public bool WifiApLock { get; set; }

        [JsonPropertyName("wifiAPPassword")]
        public string WifiApPassword { get; set; }

        [JsonPropertyName("wifiEnable")]
        public bool WifiEnable { get; set; }
    }
}