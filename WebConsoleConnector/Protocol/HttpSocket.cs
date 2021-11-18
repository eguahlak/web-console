using System;
using System.Net;
using System.Net.Sockets;

namespace WebConsoleConnector.Protocol
{
    public class HttpSocketListener : IDisposable
    {
        private readonly Socket socket;
        public int Port { get; }

        public HttpSocketListener(int port)
        {
            Port = port;
            IPAddress ipAddress = new(new byte[] { 127, 0, 0, 1 });
            IPEndPoint localEndPoint = new(ipAddress, port);

            socket = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(localEndPoint);
            socket.Listen();
        }

        public HttpSocketHandler Accept() => new(socket.Accept());

        public void Dispose()
        {
            socket.Dispose();
        }
    }

    public class HttpSocketHandler : IDisposable
    {
        private readonly Socket socket;

        public HttpSocketHandler(Socket socket)
        {
            this.socket = socket;
        }

        public void Send(IHttpResponse response)
        {
            response.SendTo(socket);
        }

        public IHttpRequest Receive()
        {
            string type = "none";
            HttpProtocolData data = new(socket);
            if (!data.Headers.ContainsKey("Content-Type")) return new HttpGetRequest(data);
            string[] typeParts = data.Headers["Content-Type"].Split(';', StringSplitOptions.TrimEntries);
            type = typeParts[0];
            switch (type)
            {
                case "text/plain": return new HttpTextRequest(data);
                default: return new HttpTextRequest(data);
            }
        }

        public void Dispose()
        {
            socket.Dispose();
        }
    }
}
