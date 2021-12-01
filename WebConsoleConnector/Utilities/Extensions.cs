using System.IO;
using System.Text;
using WebConsoleConnector.Form;
using static System.Web.HttpUtility;

namespace WebConsoleConnector.Utilities
{
    public static class Extensions
    {
        public static void AppendIndentedLine(this StringBuilder builder, string indent, string line)
        {
            if (indent == null) builder.Append(line);
            else builder.Append($"{indent}{line}\n");
        }

        public static void AppendIndentedLines(this StringBuilder builder, string indent, string fileName)
        {
            foreach (string line in File.ReadLines(fileName))
            {
                builder.AppendIndentedLine(indent, line);
            }
        }

        public static int IndexOfCrLf(this byte[] buffer, int offset, int size)
        {
            for (int i = offset; i < size - 1; i++)
            {
                if (buffer[i] == 13 && buffer[i + 1] == 10) return i;
            }
            return -1;
        }

        private static int AppendStyle(StringBuilder builder, string text, int index, ref bool activated, string name)
        {
            char here = text[index];
            char next = index < text.Length - 1 ? text[index + 1] : ' ';
            if (here == next)
            {
                builder.Append(next);
                index++;
            }
            else
            {
                builder.Append(activated ? $"</{name}>" : $"<{name}>");
                activated = !activated;
            }
            return ++index;
        }

        public static string Encoded(this string text)
        {
            text = HtmlEncode(text);
            StringBuilder result = new();
            bool bold = false;
            bool italic = false;
            bool underline = false;
            bool teletype = false;
            int index = 0;
            while (index < text.Length)
            {
                char c = text[index];
                switch (c)
                {
                    case '*': index = AppendStyle(result, text, index, ref bold, "b"); break;
                    case '/': index = AppendStyle(result, text, index, ref italic, "i"); break;
                    case '_': index = AppendStyle(result, text, index, ref underline, "u"); break;
                    case '`': index = AppendStyle(result, text, index, ref teletype, "tt"); break;
                    default:
                        result.Append(c);
                        index++;
                        break;
                }
            }
            return result.ToString();
        }


    }
}
