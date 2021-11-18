using System.Text;

namespace WebConsoleConnector.Protocol
{
    public class HttpTextResponse : HttpResponseBase
    {
        public HttpTextResponse(string data)
        {
            Content = Encoding.UTF8.GetBytes(data);
        }

        public override int Code => 200;

        public override string Message => "OK";

        public override string ContentType => "text/plain; charset=UTF-8";

    }
}
