using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using PruebaDitech.Model;

namespace PruebaDitech.Controllers
{
    public class CalculatorController: ApiController
    {
        private static Operations log = new Operations();

        [HttpPost]
        public HttpResponseMessage add([FromBody] JObject paramsObject)
        {
            try
            {
                int[] numbers = paramsObject.GetValue("Addends").ToObject<int[]>();

                IEnumerable<string> header = null;
                Request.Headers.TryGetValues("Tracking-Id", out header);
                if (header != null)
                {
                    var trackingId = Request.Headers.GetValues("Tracking-Id").ToArray()[0].ToString();

                    log.Add(
                        TrackingId: trackingId,
                        OperationType: "Sum",
                        Calculation: string.Format("Parameter:{0} Result:{1}", paramsObject.GetValue("Addends"), numbers.Sum()),
                        Date: DateTime.Now
                        );
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Sum = numbers.Sum()});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, 
                    new Errors(){
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }

        public HttpResponseMessage sub([FromBody] JObject paramsObject)
        {
            try
            {
                int Minuend = paramsObject.GetValue("Minuend").ToObject<int>();
                int Subtrahend = paramsObject.GetValue("Subtrahend").ToObject<int>();
                var result = Minuend - Subtrahend;

                IEnumerable<string> header = null;
                Request.Headers.TryGetValues("Tracking-Id", out header);
                if (header != null)
                {
                    var trackingId = Request.Headers.GetValues("Tracking-Id").ToArray()[0].ToString();

                    log.Add(
                    TrackingId: trackingId,
                    OperationType: "Sub",
                    Calculation: string.Format("Minuend:{0} Subtrahend:{1} Result:{2}", Minuend, Subtrahend, result),
                    Date: DateTime.Now
                    );
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Difference = result.ToString() });


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Errors()
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }

        public HttpResponseMessage mult([FromBody] JObject paramsObject)
        {
            try
            {
                var result = 1;
                int[] numbers = paramsObject.GetValue("Factors").ToObject<int[]>();

                foreach (int number in numbers)
                {
                    result *= number;
                }

                IEnumerable<string> header = null;
                Request.Headers.TryGetValues("Tracking-Id", out header);
                if (header != null)
                {
                    var trackingId = Request.Headers.GetValues("Tracking-Id").ToArray()[0].ToString();

                    log.Add(
                    TrackingId: trackingId,
                    OperationType: "Mult",
                    Calculation: string.Format("Parameter:{0} Result:{1}", paramsObject.GetValue("Factors"), result),
                    Date: DateTime.Now
                    );
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Product = result.ToString() });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Errors()
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }

        public HttpResponseMessage div([FromBody] JObject paramsObject)
        {
            try
            {
                int Dividend = paramsObject.GetValue("Dividend").ToObject<int>();
                int Divisor = paramsObject.GetValue("Divisor").ToObject<int>();

                IEnumerable<string> header = null;
                Request.Headers.TryGetValues("Tracking-Id", out header);
                if (header != null)
                {
                    var trackingId = Request.Headers.GetValues("Tracking-Id").ToArray()[0].ToString();

                    log.Add(
                    TrackingId: trackingId,
                    OperationType: "Div",
                    Calculation: string.Format("Dividend:{0} Divisor:{1} Result:{2}", Dividend, Divisor, (Dividend / Divisor)),
                    Date: DateTime.Now
                    );
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Quotient = (int)(Dividend / Divisor), Remainder = Dividend % Divisor });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Errors()
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }

        public HttpResponseMessage sqrt([FromBody] JObject paramsObject)
        {
            try
            {
                int Number = paramsObject.GetValue("Number").ToObject<int>();

                IEnumerable<string> header = null;
                Request.Headers.TryGetValues("Tracking-Id", out header);
                if (header != null)
                {
                    var trackingId = Request.Headers.GetValues("Tracking-Id").ToArray()[0].ToString();

                    log.Add(
                    TrackingId: trackingId,
                    OperationType: "Sqrt",
                    Calculation: string.Format("Parameter:{0} Result:{1}", Number, Math.Sqrt(Number)),
                    Date: DateTime.Now
                    );
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Square = Math.Sqrt(Number) });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Errors()
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }

        public HttpResponseMessage query([FromBody] JObject paramsObject)
        {
            try
            {
                string id = paramsObject.GetValue("Id").ToObject<string>();
                var list = new Operations();
                list.OperationList.Add(new Operation());
                list.OperationList.Add(new Operation());
                return Request.CreateResponse(HttpStatusCode.OK, log.OperationList.Where(x => x.TrackingId == id));

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Errors()
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorStatus = (int)HttpStatusCode.InternalServerError,
                        ErrorMessage = ex.Message
                    }
                    );
            }
        }
    }
}
