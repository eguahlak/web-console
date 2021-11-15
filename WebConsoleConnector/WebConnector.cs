using System;
using System.Reflection;

namespace WebConsoleConnector
{
    public class WebConnector
    {
        public static void Start(object connector, int port)
        {
            Type type = connector.GetType();
            foreach (var methodInfo in type.GetMethods())
            {
                Console.WriteLine($"{methodInfo.Name}");
                methodInfo.Invoke(connector, )
                foreach (var paramInfo in methodInfo.GetParameters())
                {
                    Console.WriteLine($"  {paramInfo.Name}: {paramInfo.ParameterType}");
                }

            }
            
            throw new NotImplementedException();

            /*
             * GET /person/7 HTTP/1.1
             * ContentType: application/json
             * ContentLength: 0
             * 
             * <content>
             */

            /*
             * HTTP/1.1 200 OK
             * ContenType: application/json
             * ContentLength: 73
             * 
             * { "@type": "TemplateBackend.Person"; "Id": 7, "Name": "Kurt", "Age": 34 } 
             */

        }
    }
}
