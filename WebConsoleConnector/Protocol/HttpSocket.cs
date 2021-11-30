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
            HttpProtocolData data = new(socket);
            switch (data.ContentType)
            {
                case null: return new HttpGetRequest(data);
                case "text/plain": return new HttpTextRequest(data);
                case "application/json": return new HttpActionRequest(data);
                default: return new HttpTextRequest(data);
            }
        }

        public void Dispose()
        {
            socket.Dispose();
        }
    }
}
