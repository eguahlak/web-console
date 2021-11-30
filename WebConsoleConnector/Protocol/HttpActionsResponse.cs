using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;
using static Newtonsoft.Json.JsonConvert;

namespace WebConsoleConnector.Protocol
{
    public class HttpActionsResponse : HttpResponseBase
    {
        public HttpActionsResponse(IList<IAction> actions) : base("application/json")
        {
            string json = SerializeObject(actions);
            data.Content = Encoding.UTF8.GetBytes(json);
        }
    }
}
