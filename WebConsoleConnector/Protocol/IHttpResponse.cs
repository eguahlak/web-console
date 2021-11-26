using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public interface IHttpResponse : IResponse
    {
        public string Protocol => "HTTP/1.1"; 
        public int Code { get; }
        public string Message { get; }

        public string ContentType { get; }
        public int ContentLength { get; }

        public IDictionary<string, string> Headers { get; }

    }
}
