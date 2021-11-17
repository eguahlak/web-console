using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public class HttpStringResponse : HttpResponseBase, IHttpResponse
    {
        public HttpStringResponse(string data)
        {
            Content = Encoding.UTF8.GetBytes(data);
        }

        public override int Code => 200;

        public override string Message => "OK";

        public override string ContentType => "text/plain; charset=UTF-8";

    }
}
