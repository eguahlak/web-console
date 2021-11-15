using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebConsoleConnector;

namespace TemplateBackend
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    
    public class PersonController
    {
        public Person GetPerson(long id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetPerson()
        {
            throw new NotImplementedException();
        }

        public void PostPerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
    


    class Program
    {
        static void Main(string[] args)
        {
            //PersonController controller = new PersonController();
            //WebConnector.Start(controller, 4711);

            byte[] buffer = new byte[1_024];

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            //IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4711);

            Socket serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(localEndPoint);
            serverSocket.Listen();
            Console.WriteLine("Waiting ...");
            using Socket socket = serverSocket.Accept();
            string data = "";
            //while (true)
            //{
                int byteCount = socket.Receive(buffer);
                data += Encoding.UTF8.GetString(buffer, 0, byteCount);
            //    if (data.IndexOf("\n\n") > -1) break;
            //}

            Console.WriteLine(data);

            byte[] message = Encoding.UTF8.GetBytes("Hello there");

            socket.Send(message);
            // socket.Shutdown(SocketShutdown.Both);
            // socket.Close();



        }
    }
}
