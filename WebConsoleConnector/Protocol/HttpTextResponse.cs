using System.Text;

namespace WebConsoleConnector.Protocol
{
    public class HttpTextResponse : HttpResponseBase
    {
        public HttpTextResponse(string text) : base("text/plain")
        {
            data.Content = Encoding.UTF8.GetBytes(text);
        }

    }
}
