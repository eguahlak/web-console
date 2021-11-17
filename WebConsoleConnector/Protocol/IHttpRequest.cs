using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public interface IHttpRequest
    {
        public string Method { get; }
        public string Resource { get; }
        public string Protocol { get; }

        public int ContentLength { get; }
        public string ContentType { get; }

        public IDictionary<string, string> Headers { get; }

        public byte[] Content { get; }
    }
}
