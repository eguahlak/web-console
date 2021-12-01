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
