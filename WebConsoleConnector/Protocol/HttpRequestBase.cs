using System;
using System.Collections.Generic;

namespace WebConsoleConnector.Protocol
{
    public abstract class HttpRequestBase : IHttpRequest
    {
        protected HttpProtocolData data;

        public HttpRequestBase(HttpProtocolData data) 
        {
            this.data = data;
            string[] parts = data.SignatureLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Method = parts[0];
            Resource = parts[1];
            Protocol = parts[2];
        }

        public string Method { get; }

        public string Resource { get; }

        public string Protocol { get; }

        public int ContentLength => data.Content.Length;

        public string ContentType => data.ContentType;

        public IDictionary<string, string> Headers => data.Headers;

    }
}
