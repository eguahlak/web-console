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
        private static void AppendStyle(StringBuilder builder, ref char last, char next, ref bool activated, string name)
        {
            if (next == last)
            {
                builder.Append(next);
                last = ' ';
            }
            else
            {
                builder.Append(activated ? $"</{name}>" : $"<{name}>");
                last = next;
                activated = !activated;
            }
        }

        public static string Encoded(this string text)
        {
            StringBuilder result = new();
            bool bold = false;
            bool italic = false;
            bool underline = false;
            char last = ' '; 
            foreach (char c in text)
            {
                switch (c)
                {
                    case '*': AppendStyle(result, ref last, c, ref bold, "b"); break;
                    case '/': AppendStyle(result, ref last, c, ref italic, "i"); break;
                    case '_': AppendStyle(result, ref last, c, ref underline, "u"); break;
                    default:
                        result.Append(c);
                        last = c;
                        break;
                }
            }
            return HtmlEncode(result.ToString());
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

        public Text(int level)
        {
            this.level = level;
        }

        public Text() : this(0) { }

        public override void Accept(StringBuilder builder, string indent)
        {
            if (level == 0) builder.AppendIndentedLine(indent, $"<p id='{Id}'>{Content.Encoded()}</p>");
            else builder.AppendIndentedLine(indent, $"<h{level} id='{Id}'>{Content.Encoded()}</h{level}>");
        }

        public override bool Handle(IAction action) => false;
    }
}
