using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ApexLoader.ApexOlog
{

    public class Root
    {
        [JsonPropertyName("olog")]
        public Olog Olog { get; set; }
    }
}