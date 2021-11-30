using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Utilities;
using System.Collections;
using WebConsoleConnector.Protocol;
using WebConsoleConnector.Form.Actions;
using System.Threading;
using System.IO;
using System.Reflection;

namespace WebConsoleConnector.Form
{
    public class HttpForm : ComponentBase, IParent
    {
        public string Title { get; set; }

        public static IDictionary<Guid, IComponent> Components { get; } = new Dictionary<Guid, IComponent>();

        public static IList<IAction> Actions { get; } = new List<IAction>();

        public static void WriteLine(string entry)
        {
            Console.WriteLine(entry);
        } 

        public IList<IChild> Children { get; } = new List<IChild>();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public HttpForm(string title, params IChild[] children) : base()
        {
            Title = title;
            foreach (var child in children) Add(child);
        }

        public void Add(IChild child)
        {
            child.Parent = this;
            HttpForm.Components[child.Id] = child;
            Children.Add(child);
        }



        public override void Accept(StringBuilder builder, string indent)
        {
            string rootPath = Assembly.GetExecutingAssembly().Location;
            
//            string fullFilePath = Path.Combine((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath.Split(new string[] { "/bin" }, StringSplitOptions.None)[0]
//                          , "@/Images/test.png");

            builder.AppendIndentedLine("", $"<!DOCTYPE>");
            builder.AppendIndentedLine("", $"<html>");
            builder.AppendIndentedLine("  ", $"<head>");
            builder.AppendIndentedLine("    ", $"<title>{Title}</title>");
            builder.AppendIndentedLine("    ", "<style>");
            builder.AppendIndentedLines("      ", @"Form\Files\styles.css");
            builder.AppendIndentedLine("    ", "</style>");
            builder.AppendIndentedLine("    ", "<script>");
            builder.AppendIndentedLines("      ", @"Form\Files\scripts.js");
            builder.AppendIndentedLine("    ", "</script>");
            builder.AppendIndentedLine("  ", $"</head>");
            builder.AppendIndentedLine("  ", $"<body onload='init();'>");
            foreach (var child in Children) child.Accept(builder, "    ");
            builder.AppendIndentedLine("  ", $"</body>");
            builder.AppendIndentedLine("", $"</html>");
        }

        public override void Accept(StringBuilder builder) => Accept(builder, "");

        private int port = 4711;

        private void DoListen()
        {
            using HttpSocketListener listener = new(port);
            WriteLine($"Listening on port #{port}");
            while (true)
            {
                using HttpSocketHandler handler = listener.Accept();

                IHttpRequest request = handler.Receive();
                IHttpResponse response;
                if (request is HttpGetRequest httpGet)
                {
                    switch (httpGet.Resource)
                    {
                        case "/actions":
                            lock (Actions)
                            {
                                response = new HttpActionsResponse(Actions);
                                if (Actions.Count > 0)
                                {
                                    Actions.Clear();
                                    WriteLine($"Processed '{httpGet.Resource}'");
                                }
                            }
                            break;
                        default:
                            lock (Actions)
                            {
                                Actions.Clear();
                                response = new HttpHtmlResponse(this);
                                WriteLine($"Processed '{httpGet.Resource}'");
                            }
                            break;
                    }
                }
                else if (request is HttpActionRequest httpEvent)
                {
                    WriteLine($"Processing event from '{httpEvent.Event.Id}' ...");
                    Guid id = httpEvent.Event.Id;
                    var component = Components[id];
                    bool success = component.Handle(httpEvent.Event);
                    if (success) response = new HttpResponseBase(204);
                    else response = new HttpResponseBase(405);
                }
                else response = new HttpHtmlResponse(this);
                handler.Send(response);
            }
        }

        public void Publish(int port)
        {
            this.port = port;
            Thread listener = new(DoListen);
            listener.Start();
        }

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction e) => false;

        public void Publish() => Publish(4711);

    }
}
