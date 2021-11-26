using System;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class TextField : ChildBase
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                HttpForm.Events.Add(new UpdateAction(Id, value));
            }
        }

        public bool Editable { get; }

        public TextField(string value, bool editable)
        {
            this.value = value;
            Editable = editable;
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            if (Editable)
                builder.AppendIndentedLine(indent, $"<input id='{Id}' value='{Value}' onchange='sendUpdateAction(this);' />");
            else 
                builder.AppendIndentedLine(indent, $"<input readonly='readonly' id='{Id}' value='{Value}' />");
        }

        public override bool Handle(IAction action)
        {
            if (action is UpdateAction updateAction)
            {
                value = updateAction.Value;
                OnUpdate?.Invoke(this, value);
                return true;
            }
            return false;
        }
    }
}
