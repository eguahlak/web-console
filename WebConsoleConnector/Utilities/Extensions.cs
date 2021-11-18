using System.Text;
using static System.Web.HttpUtility;

namespace WebConsoleConnector.Utilities
{
    public static class Extensions
    {
        public static string Encoded(this string text) => HtmlEncode(text);

        public static void AppendIndentedLine(this StringBuilder builder, string indent, string line) =>
            builder.Append($"{indent}{line}\n");

    }
}
