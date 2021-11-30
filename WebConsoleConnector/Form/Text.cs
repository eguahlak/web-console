using System;
using System.Text;
using System.Text.Encodings.Web;
using static System.Web.HttpUtility;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public static class TextEncoder
    {
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
            //return HtmlEncode(result.ToString());
        }
    }


    public class Text : ChildBase
    {
        private string content;

        private int level;

        public string Content
        {
            get => content;
            set
            {
                this.content = value;
                HttpForm.Actions.Add(new UpdateAction(Id, content));
            }
        }

        public Text(string content, int level)
        {
            this.content = content;
            this.level = level;
        }

        public Text(string content) : this(content, 0) { }

        public override void Accept(StringBuilder builder, string indent)
        {
            if (level == 0) builder.AppendIndentedLine(indent, $"<p id='{Id}'>{Content.Encoded()}</p>");
            else builder.AppendIndentedLine(indent, $"<h{level} id='{Id}'>{Content.Encoded()}</h{level}>");
        }

        public override bool Handle(IAction action) => false;
    }
}
