using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WebConsoleConnector.Protocol
{
    public abstract class HttpResponseBase : IHttpResponse
    {
        protected HttpProtocolData data;

        public string Protocol => "HTTP/1.1";

        public int Code { get; }

        public string Message { get; }

        public string ContentType => data.ContentType;

        public int ContentLength => data.ContentLength;

        public IDictionary<string, string> Headers => data.Headers;

        public HttpResponseBase(string contentType)
        {
            Code = 200;
            Message = "OK";
            data = new($"{Protocol} {Code} {Message}", contentType);
        }

        public HttpResponseBase(int code, string message)
        {
            Code = code;
            Message = message;
            data = new($"{Protocol} {Code} {Message}", null);
        }

        public void SendTo(Socket handler) => data.SendTo(handler);
    }
}
