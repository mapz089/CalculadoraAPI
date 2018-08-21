using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDitech.Model
{
    public class Operation
    {
        public string TrackingId { get; set; }

        [JsonProperty(PropertyName = "Operation")]
        public string OperationType { get; set; }
        public string Calculation { get; set; }
        public DateTime? Date { get; set; }
        
    }
}
