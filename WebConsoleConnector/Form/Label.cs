using System;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Label : ChildBase
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                HttpForm.Actions.Add(new UpdateAction(Id, value));
            }
        }

        public Label(string value)
        {
            this.value = value;
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine(indent, $"<span class='Label' id='{Id}'>{Value}</span>");
        }

        public override bool Handle(IAction formEvent) => false;

    }
}
