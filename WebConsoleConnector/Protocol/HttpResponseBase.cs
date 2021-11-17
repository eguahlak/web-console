using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public abstract class HttpResponseBase : IHttpResponse
    {
        public string Protocol => "HTTP/1.1";
        public abstract int Code { get; }
        public abstract string Message { get; }
        public abstract string ContentType { get; }
        public int ContentLength => Content == null ? 0 : Content.Length;

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        
        public byte[] Content { get; protected set; }

        private void SendASCIILine(Socket handler, string text)
        {
            handler.Send(Encoding.ASCII.GetBytes(text + "\r\n"));
        }

        public void SendTo(Socket handler)
        {
            SendASCIILine(handler, $"{Protocol} {Code} {Message}");
            SendASCIILine(handler, $"Content-Type: {ContentType}");
            SendASCIILine(handler, $"Content-Length: {ContentLength}");
            foreach (KeyValuePair<string, string> header in Headers)
            {
                SendASCIILine(handler, $"{header.Key}: {header.Value}");
            }
            SendASCIILine(handler, "");
            handler.Send(Content);
        }
    }
}
