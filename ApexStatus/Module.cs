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

    using global::System.Text.Json.Serialization;

    #endregion

    public class Module
    {
        [JsonPropertyName("abaddr")]
        public int Abaddr { get; set; }

        [JsonPropertyName("boot")]
        public bool Boot { get; set; }

        [JsonPropertyName("extra")]
        public Extra2 Extra { get; set; }

        [JsonPropertyName("hwrev")]
        public int Hwrev { get; set; }

        [JsonPropertyName("hwtype")]
        public string Hwtype { get; set; }

        [JsonPropertyName("inact")]
        public int Inact { get; set; }

        [JsonPropertyName("pcount")]
        public int Pcount { get; set; }

        [JsonPropertyName("perror")]
        public int Perror { get; set; }

        [JsonPropertyName("pgood")]
        public int Pgood { get; set; }

        [JsonPropertyName("present")]
        public bool Present { get; set; }

        [JsonPropertyName("reatt")]
        public int Reatt { get; set; }

        [JsonPropertyName("swrev")]
        public int Swrev { get; set; }

        [JsonPropertyName("swstat")]
        public string Swstat { get; set; }
    }
}