using System.IO;
using System.Text;
using WebConsoleConnector.Form;
using static System.Web.HttpUtility;

namespace WebConsoleConnector.Utilities
{
    public static class Extensions
    {
        public static string Encoded(this string text) => HtmlEncode(text);

        public static void AppendIndentedLine(this StringBuilder builder, string indent, string line)
        {
            if (indent == null) builder.Append(line);
            else builder.Append($"{indent}{line}\n");
        }

        //public static void AppendIndentedLines(this StringBuilder builder, string indent, )
        //{

        //}

        public static int IndexOfCrLf(this byte[] buffer, int offset, int size)
        {
            for (int i = offset; i < size - 1; i++)
            {
                if (buffer[i] == 13 && buffer[i + 1] == 10) return i;
            }
            return -1;
        }

    }
}
