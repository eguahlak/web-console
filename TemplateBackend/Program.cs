using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebConsoleConnector;
using WebConsoleConnector.Protocol;

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
            IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4711);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen();
            Console.WriteLine("Waiting ...");
            using Socket handler = listener.Accept();
            HttpRequest request = new(handler);

            string message = $"Received a {request.Method} on {request.Resource} with content length {request.ContentLength}";

            HttpStringResponse response = new(message);
            response.SendTo(handler);
            
            // handler.Send(Encoding.UTF8.GetBytes(message));
            // socket.Shutdown(SocketShutdown.Both);
            // socket.Close();



        }
    }
}
