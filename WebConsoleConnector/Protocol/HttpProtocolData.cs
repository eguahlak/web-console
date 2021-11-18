using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using static WebConsoleConnector.Utilities.Extensions;

namespace WebConsoleConnector.Protocol
{
    public class HttpProtocolData
    {
        public const int BUFFER_SIZE = 1_024;

        public string SignatureLine { get; private set; }

        public int ContentLength => Content == null ? 0 : Content.Length;

        public string ContentType
        {
            get
            {
                if (!Headers.ContainsKey("Content-Type")) return null;
                return Headers["Content-Type"].Split(';', StringSplitOptions.TrimEntries)[0];
            }
            set
            {
                if (value == null) Headers.Remove("Content-Type");
                else Headers["Content-Type"] = $"{value}; charset=UTF-8";
            }
        }

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        private byte[] content = null;

        public byte[] Content
        {
            get => content;
            set
            {
                Headers["Content-Length"] = value.Length.ToString();
                content = value;
            }
        }

        public HttpProtocolData(Socket socket)
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int contentLength = 0;

            int offset = 0;
            int count = socket.Receive(buffer);
            int lineNumber = 0;
            string line = null;
            
            while (true)
            {
                int lineEnd = buffer.IndexOfCrLf(offset, count);
                if (lineEnd >= 0)
                { // line end found
                    int lineLenght = lineEnd - offset;
                    line += Encoding.ASCII.GetString(buffer, offset, lineLenght);
                    offset = lineEnd + 2;
                    if (line == null || line.Length == 0) break;
                    if (lineNumber == 0) SignatureLine = line;
                    else
                    {
                        int split = line.IndexOf(':');
                        string headerKey = line.Substring(0, split).Trim();
                        string headerValue = line.Substring(split + 1).Trim();
                        Headers.Add(headerKey, headerValue);
                        if (headerKey == "Content-Length") contentLength = int.Parse(headerValue);
                    }
                    line = null;
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
            Content = new byte[contentLength];
            int index = count - offset;
            if (index > 0) Array.Copy(buffer, offset, Content, 0, index);
            while (true)
            {
                if (index == Content.Length) break;
                count = socket.Receive(Content, index, Content.Length - index, SocketFlags.None);
                index += count;
            }
        } 

        public HttpProtocolData(string signatureLine, string contentType)
        {
            SignatureLine = signatureLine;
            ContentType = contentType;
        }

        private void SendASCIILine(Socket handler, string text)
        {
            handler.Send(Encoding.ASCII.GetBytes(text + "\r\n"));
        }

        public void SendTo(Socket handler)
        {
            SendASCIILine(handler, SignatureLine);
            foreach (KeyValuePair<string, string> header in Headers)
            {
                SendASCIILine(handler, $"{header.Key}: {header.Value}");
            }
            SendASCIILine(handler, "");
            handler.Send(Content);
        }


    }
}
