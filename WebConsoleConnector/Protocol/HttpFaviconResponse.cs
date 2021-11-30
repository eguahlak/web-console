using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public class HttpFaviconResponse : HttpResponseBase 
    {
        public HttpFaviconResponse() : base("image/png")
        {
            data.Content = File.ReadAllBytes("Form/Files/favicon.ico");
        }
    }
}
