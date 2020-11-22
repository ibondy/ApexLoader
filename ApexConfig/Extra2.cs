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

namespace ApexLoader.ApexConfig
{
    #region using

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    #endregion

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