using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Protocol
{
    public class HttpRequest : IHttpRequest
    {
        private static int IndexOfCrLf(byte[] buffer, int offset, int size)
        {
            for (int i = offset; i < size - 1; i++)
            {
                if (buffer[i] == 13 && buffer[i + 1] == 10) return i;
            }
            return -1;
        }


        private void ProcessResourceLine(string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Method = parts[0];
            Resource = parts[1];
            Protocol = parts[2];
        }

        private void ProcessHeaderLine(string line)
        {
            int split = line.IndexOf(':');
            string headerKey = line.Substring(0, split).Trim();
            string headerValue = line.Substring(split + 1).Trim();
            Headers.Add(headerKey, headerValue);
            switch (headerKey.ToLower())
            {
                case "content-length":
                    ContentLength = int.Parse(headerValue);
                    break;
                case "content-type":
                    ContentType = headerValue;
                    break;
            }


        }

        public HttpRequest(Socket socket)
        {
            byte[] buffer = new byte[1_024];
            // byte[] buffer = new byte[128];

            int offset = 0;
            int count = socket.Receive(buffer);
            int lineNumber = 0;
            string line = null;
            
            while (true)
            {
                int lineEnd = IndexOfCrLf(buffer, offset, count);
                if (lineEnd >= 0)
                { // line end found
                    int lineLenght = lineEnd - offset;
                    line += Encoding.ASCII.GetString(buffer, offset, lineLenght);
                    if (line == null || line.Length == 0) break;
                    if (lineNumber == 0) ProcessResourceLine(line);
                    else ProcessHeaderLine(line);
                    line = null;
                    offset = lineEnd + 2;
                    lineNumber++;
                }
                else
                {
                    int lineLength = count - offset;
                    line += Encoding.ASCII.GetString(buffer, offset, lineLength);
                    offset = 0;
                    count = socket.Receive(buffer);
                }
            }
            // process body goes here
        } 

        public string Method { get; private set; }

        public string Resource { get; private set; }

        public string Protocol { get; private set; }

        public int ContentLength { get; private set; }

        public string ContentType { get; private set; }

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public byte[] Content => throw new NotImplementedException();
    }
}
