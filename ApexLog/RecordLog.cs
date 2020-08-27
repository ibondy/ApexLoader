using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexLoader.ApexLog
{
    public class RecordLog :Datum
    {
        public DateTime DateTime { get; set; }
        public string ApexId { get; set; }

        //[JsonProperty("id")]
        //public string Id { get; set; }

        public RecordLog() { }
        public RecordLog(DateTime dateTime, string apexId, Datum datum)
        {
            this.Did = datum.Did;
            this.Name = datum.Name;
            this.Type = datum.Type;
            this.Value = datum.Value;
            this.ApexId = apexId;
            this.DateTime = dateTime;
            //this.Id = Guid.NewGuid().ToString();
        }
    }
}
