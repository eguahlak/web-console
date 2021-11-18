using System;
using System.Text;
using WebConsoleConnector.Form;

namespace WebConsoleConnector.Protocol
{
    public class HttpHtmlResponse : HttpResponseBase
    {
        public HttpHtmlResponse(HtmlForm form)
        {
            StringBuilder builder = new();
            form.Accept(builder, "");
            Content = Encoding.UTF8.GetBytes(builder.ToString());
        }

        public override int Code => 200;

        public override string Message => "OK";

        public override string ContentType => "text/html; charset=UTF-8";
    }
}
