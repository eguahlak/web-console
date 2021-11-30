using WebConsoleConnector.Form.Actions;
using static Newtonsoft.Json.JsonConvert;

namespace WebConsoleConnector.Protocol
{
    public class HttpActionRequest : HttpTextRequest
    {
        public IAction Event => DeserializeObject<IAction>(Text);

        public HttpActionRequest(HttpProtocolData data) : base(data) { }
    }
}
