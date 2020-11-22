#region Copyright

// MIT License
// 
// Copyright (c) 2020 Ivan Bondy
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

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

        [JsonPropertyName("dns")]
        public List<string> Dns { get; set; }

        [JsonPropertyName("emailPassword")]
        public string EmailPassword { get; set; }

        [JsonPropertyName("fusionEnable")]
        public bool FusionEnable { get; set; }

        [JsonPropertyName("gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("httpPort")]
        public int HttpPort { get; set; }

        [JsonPropertyName("ipaddr")]
        public string Ipaddr { get; set; }

        [JsonPropertyName("latestFirmware")]
        public string LatestFirmware { get; set; }

        [JsonPropertyName("link")]
        public bool Link { get; set; }

        [JsonPropertyName("netmask")]
        public string Netmask { get; set; }

        [JsonPropertyName("quality")]
        public int Quality { get; set; }

        [JsonPropertyName("ssid")]
        public string Ssid { get; set; }

        [JsonPropertyName("strenght")]
        public int Strength { get; set; }

        [JsonPropertyName("updateFirmware")]
        public bool UpdateFirmware { get; set; }

        [JsonPropertyName("wifiAP")]
        public bool WifiAp { get; set; }

        [JsonPropertyName("wifiAPLock")]
        public bool WifiApLock { get; set; }

        [JsonPropertyName("wifiPassword")]
        public string WifiApPassword { get; set; }

        [JsonPropertyName("wifiEnable")]
        public bool WifiEnable { get; set; }
    }
}