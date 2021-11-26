using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;
using static Newtonsoft.Json.JsonConvert;

namespace WebConsoleConnector.Protocol
{
    public class HttpEventsResponse : HttpResponseBase
    {
        public HttpEventsResponse(IList<IAction> events) : base("application/json")
        {
            string json = SerializeObject(events);
            data.Content = Encoding.UTF8.GetBytes(json);
        }
    }
}
