using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexLoader.ApexTLog
{
    public class Root
    {
        [JsonPropertyName("tlog")]
        public Tlog Tlog { get; set; }
    }
}