using System;
using System.Text;
using WebConsoleConnector.Form.Actions;
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
            builder.AppendIndentedLine(indent, $"<button id='{Id}'{State} onclick='sendClickAction(this);'>{Title.Encoded()}</button>");
        }

        public override bool Handle(IAction action)
        {
            if (action is ClickAction clickAction)
            {
                if (OnClick == null)
                {
                    Console.WriteLine($"Button {Title}/{Id} clicked, but no handler found!");
                }
                else
                {
                    OnClick(this);
                }
                return true;
            }
            return false;
        }
    }
}
