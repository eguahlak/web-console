using System.Text;
using WebConsoleConnector.Form;

namespace WebConsoleConnector.Protocol
{
    public class HttpHtmlResponse : HttpResponseBase
    {
        public HttpHtmlResponse(HttpForm form) : base("text/html")
        {
            StringBuilder builder = new();
            form.Accept(builder, "");
            data.Content = Encoding.UTF8.GetBytes(builder.ToString());
        }

    }
}
