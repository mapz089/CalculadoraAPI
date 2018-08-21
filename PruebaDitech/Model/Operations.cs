using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDitech.Model
{
    public class Operations
    {
        [JsonProperty(PropertyName = "Operations")]
        public List<Operation> OperationList {get;set;}

        public Operations()
        {
            this.OperationList = new List<Operation>();
        }

        public void Add(string TrackingId, string OperationType, string Calculation, DateTime Date)
        {
            this.OperationList.Add(
                new Operation()
                {
                    TrackingId = TrackingId,
                    OperationType = OperationType,
                    Calculation = Calculation,
                    Date = Date
                }
            );
        }
    }
}