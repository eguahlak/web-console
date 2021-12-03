using WebConsoleConnector.Form;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Protocol
{
    public class HttpFaviconResponse : HttpResponseBase 
    {
        public HttpFaviconResponse() : base("image/png")
        {
            data.Content = EmbeddedResource<HttpForm>.ReadAllBytes("Form/Files/favicon.ico");
        }
    }
}
