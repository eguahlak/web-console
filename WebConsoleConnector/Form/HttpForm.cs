using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Utilities;
using System.Collections;
using WebConsoleConnector.Protocol;
using WebConsoleConnector.Form.Actions;
using System.Threading;

namespace WebConsoleConnector.Form
{
    public class HttpForm : ComponentBase, IParent
    {
        public string Title { get; set; }

        public static IDictionary<Guid, IComponent> Components { get; } = new Dictionary<Guid, IComponent>();

        public static IList<IAction> Events { get; } = new List<IAction>();

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

        private string script =
@"
    <script>
      function init() {
        window.setInterval(getEvents, 1000);
        document.body.style.backgroundColor = ""#ffeeee"";
        }

      function update(id, value) {
        let element = document.getElementById(id);
        switch (element.tagName) {
          case 'SPAN':
            element.innerHTML = value;
            break;
          case 'INPUT':
            element.value = value;
            break;
          case 'BUTTON':
            element.innerHTML = value;
            break;
          }
        }

      function getEvents() {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function() {
          if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            // alert(this.responseText);
            let events = JSON.parse(this.responseText);
            for (index in events) {
              let e = events[index];
              update(e.Id, e.Value);
              }
            }
          };
        xhttp.open('GET', 'events');
        xhttp.send();
        }

      function sendAction(e) {
        var xhttp = new XMLHttpRequest();
        xhttp.open('POST', '/events');
        xhttp.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        xhttp.onreadystatechange = function() {
          if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            // Nothing to do yet (fire and forget)
            }
          }
        xhttp.send(JSON.stringify(e));
        }

      function sendClickAction(element) {
        sendAction({ $type: 'ClickAction', Id: element.id });
        }

      function sendUpdateAction(element) {
        sendAction({ $type: 'UpdateAction', Id: element.id, Value: element.value });
        } 
    </script>
";

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine("", $"<!DOCTYPE>");
            builder.AppendIndentedLine("", $"<html>");
            builder.AppendIndentedLine("  ", $"<head>");
            builder.AppendIndentedLine("    ", $"<title>{Title}</title>");
            builder.AppendIndentedLine("", script);
            builder.AppendIndentedLine("  ", $"</head>");
            builder.AppendIndentedLine("  ", $"<body onload='init();'>");
            foreach (var child in Children) child.Accept(builder, "    ");
            builder.AppendIndentedLine("  ", $"</body>");
            builder.AppendIndentedLine("", $"</html>");
        }

        public string ToHtml()
        {
            StringBuilder builder = new();
            Accept(builder, "");
            return builder.ToString();
        }

        private int port = 4711;

        private void DoListen()
        {
            using HttpSocketListener listener = new HttpSocketListener(port);
            while (true)
            {
                WriteLine("Waiting ...");
                using HttpSocketHandler handler = listener.Accept();

                IHttpRequest request = handler.Receive();
                IHttpResponse response;
                if (request is HttpGetRequest httpGet)
                {
                    WriteLine($"Processing '{httpGet.Resource}' ...");
                    switch (httpGet.Resource)
                    {
                        case "/events":
                            lock (Events)
                            {
                                response = new HttpEventsResponse(Events);
                                Events.Clear();
                            }
                            break;
                        default:
                            lock (Events)
                            {
                                Events.Clear();
                                response = new HttpHtmlResponse(this);
                            }
                            break;
                    }
                }
                else if (request is HttpEventRequest httpEvent)
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
