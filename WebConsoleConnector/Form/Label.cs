using System;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Label : ChildBase
    {
        private string value;

        public Width Width { get; set; } = new(0.0);

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
            builder.AppendIndentedLine(indent, $"<div class='Label' style='{Width}' id='{Id}'>{Value}</div>");
        }

        public override bool Handle(IAction formEvent) => false;

    }
}
