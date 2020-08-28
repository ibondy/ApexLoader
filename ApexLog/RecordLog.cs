using System;

namespace ApexLoader.ApexLog
{
    public class RecordLog : Datum
    {
        public RecordLog()
        {
        }

        //[JsonProperty("id")]
        //public string Id { get; set; }
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

        public string ApexId { get; set; }
        public DateTime DateTime { get; set; }
    }
}