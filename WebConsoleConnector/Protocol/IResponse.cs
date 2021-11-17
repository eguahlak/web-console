using System.Net.Sockets;

namespace WebConsoleConnector.Protocol
{
    public interface IResponse
    {
        public void SendTo(Socket handler);
    }
}
