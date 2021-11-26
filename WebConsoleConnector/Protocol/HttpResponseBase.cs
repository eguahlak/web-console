using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WebConsoleConnector.Protocol
{
    public class HttpResponseBase : IHttpResponse
    {
        protected static Dictionary<int, string> Messages = new Dictionary<int, string>
        {
            [200] = "OK",
            [201] = "Created",
            [204] = "No Content",

            [404] = "Not Found",
            [405] = "Method Not Allowed"
        };

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

        public HttpResponseBase(int code) : this(code, Messages[code]) { }

        public void SendTo(Socket handler) => data.SendTo(handler);
    }
}
