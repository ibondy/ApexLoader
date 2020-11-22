using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ApexLoader.ApexDLog
{
    public class Root
    {
        [JsonPropertyName("dlog")]
        public Dlog Dlog { get; set; }
    }
}