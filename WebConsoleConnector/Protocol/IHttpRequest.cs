using System.Collections.Generic;

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

    }
}
