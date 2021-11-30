using System;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Field : ChildBase
    {
        private string value;

        public Width Width { get; set; }

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                HttpForm.Actions.Add(new UpdateAction(Id, value));
            }
        }

        public string Hint { get; }

        public bool Editable { get; }

        public Field(string value, string hint, bool editable)
        {
            this.value = value;
            Hint = hint == null ? "" : $"placeholder='{hint}' ";
            Editable = editable;
        }

        public Field(string value, bool editable) : this(value, null, editable) { }

        public Field(string value, string hint) : this(value, hint, true) { }

        public Field(string value) : this(value, null, true) { }

        public override void Accept(StringBuilder builder, string indent)
        {
            if (Editable)
                builder.AppendIndentedLine(indent, $"<input class='Field' style='{Width}' id='{Id}' {Hint}value='{Value}' onchange='sendUpdateAction(this);' />");
            else 
                builder.AppendIndentedLine(indent, $"<input class='Field' style='{Width}' id='{Id}' {Hint}readonly='readonly' value='{Value}' />");
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
