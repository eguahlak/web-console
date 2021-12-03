using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Protocol;
using WebConsoleConnector.Utilities;

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
            builder.AppendIndentedLine("", $"<!DOCTYPE>");
            builder.AppendIndentedLine("", $"<html>");
            builder.AppendIndentedLine("  ", $"<head>");
            builder.AppendIndentedLine("    ", $"<title>{Title}</title>");
            builder.AppendIndentedLine("    ", "<style>");
            builder.AppendIndentedLines("      ", @"Form/Files/styles.css");
            builder.AppendIndentedLine("    ", "</style>");
            builder.AppendIndentedLine("    ", "<script>");
            builder.AppendIndentedLines("      ", @"Form/Files/scripts.js");
            builder.AppendIndentedLine("    ", "</script>");
            builder.AppendIndentedLine("  ", $"</head>");
            builder.AppendIndentedLine("  ", $"<body onload='init();'>");
            foreach (var child in Children) child.Accept(builder, "    ");
            builder.AppendIndentedLine("  ", $"</body>");
            builder.AppendIndentedLine("", $"</html>");
        }

        public override void Accept(StringBuilder builder) => Accept(builder, "");

        private static IHttpResponse CreateActionsResponse()
        {
            lock (Actions)
            {
                IHttpResponse response = new HttpActionsResponse(Actions);
                Actions.Clear();
                return response;
            }
        }

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
                        case "/favicon.ico":
                            response = new HttpFaviconResponse();
                            break;
                        case "/actions":
                            response = CreateActionsResponse();
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
                    if (id != Id)
                    {
                        var component = Components[id];
                        bool success = component.Handle(httpEvent.Event);
                        // if (success) response = new HttpResponseBase(204);
                        if (success) response = CreateActionsResponse();
                        else response = new HttpResponseBase(405);
                    }
                    else if (httpEvent.Event is HaltAction) return;
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

        public async Task PublishAsync(int port)
        {
            this.port = port;
            await Task.Run(DoListen);
        }

        public void Halt(string reason)
        {
            lock(Actions)
            {
                // Actions.Clear();
                Actions.Add(new HaltAction(Id, reason));
            }
        }

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction e) => false;

        public void Publish() => Publish(4711);

    }
}
