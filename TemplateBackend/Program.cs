using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebConsoleConnector;
using WebConsoleConnector.Form;
using WebConsoleConnector.Protocol;
using WebConsoleConnector.Utilities;

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
        private static void TrySockets()
        {
            using HttpSocketListener listener = new HttpSocketListener(4711);
            Console.WriteLine("Waiting ...");
            using HttpSocketHandler handler = listener.Accept();

            string content;
            IHttpRequest request = handler.Receive();
            switch (request)
            {
                case HttpTextRequest text:
                    content = text.Text; break;
                default: content = "*** Unknown ***"; break;
            }

            string message = $"Received a {request.Method} on {request.Resource} with {content} length {request.ContentLength}";

            HttpTextResponse response = new(message);
            handler.Send(response);
        }

        private static void TryForms()
        {
            var form = new HtmlForm("Copy Instrument")
            {
                new Button("Press when done"),
                new Panel()
                {
                    new Button("Continue"),
                    new Button("Cancel")

                }
            };
            StringBuilder builder = new();
            form.Accept(builder, "");
            Console.WriteLine(builder.ToString());
        }

        private static void TrySocketsWithForm()
        {
            using HttpSocketListener listener = new HttpSocketListener(4711);
            while (true)
            {
                Console.WriteLine("Waiting ...");
                using HttpSocketHandler handler = listener.Accept();

                IHttpRequest request = handler.Receive();

                var form = new HtmlForm("Copy Instrument")
                {
                    new Button("Press when done"),
                    new Panel()
                    {
                        new Button($"Visit {request.Method} {request.Resource}"),
                        new Button("Cancel")
                    }
                };

                HttpHtmlResponse response = new(form);
                handler.Send(response);
            }
        }

        static void Main(string[] args)
        {
            // TrySockets();
            // TryForms();
            TrySocketsWithForm();
        }
    }
}
