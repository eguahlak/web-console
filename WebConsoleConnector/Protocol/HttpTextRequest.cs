using System;
using System.Text;

namespace WebConsoleConnector.Protocol
{
    public class HttpTextRequest : HttpRequestBase
    {

        public string Text => Encoding.UTF8.GetString(data.Content);

        public HttpTextRequest(HttpProtocolData data) : base(data) { }

    }
}
