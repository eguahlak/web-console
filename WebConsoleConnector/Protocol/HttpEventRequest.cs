using WebConsoleConnector.Form.Actions;
using static Newtonsoft.Json.JsonConvert;

namespace WebConsoleConnector.Protocol
{
    public class HttpEventRequest : HttpTextRequest
    {
        public IAction Event => DeserializeObject<IAction>(Text);

        public HttpEventRequest(HttpProtocolData data) : base(data) { }
    }
}
