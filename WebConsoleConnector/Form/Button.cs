using System;
using System.Text;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Button : ChildBase
    {
        public string Title { get; }

        public Button(string title)
        {
            Title = title;
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine(indent, $"<button id='{Id}'>{Title.Encoded()}</button>");
        }

    }
}
