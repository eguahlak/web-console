using System;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{

    public enum SelectionType
    {
        Dropdown,
        List,
        RadioCheck
    }


    public class Option : ChildBase
    {
        public bool Selected { get; set; }
        public string Value { get; }
        public Selection Selection { get; }

        public Option(Selection selection, string value, bool selected)
        {
            Selection = selection;
            Value = value;
            Selected = selected;
        }

        public Option(Selection selection, string value) : this(selection, value, false) { }

        public override void Accept(StringBuilder builder, string indent)
        {
            switch ((Parent as Selection).Type)
            {
                case SelectionType.Dropdown:
                    builder.AppendIndentedLine(indent, $"<option id='{Id}'>{Value}</option>");
                    break;
            }
        }

        public override bool Handle(IAction action)
        {
            switch (action)
            {
                case ClickAction clickAction:
                    OnClick?.Invoke(this);
                    return true;
            }
            return false;
        }
    }

    public class Selection : ChildBase
    {
        public SelectionType Type { get; }
        public bool MultiSelect { get; }
        public List<Option> Options { get; } = new();

        public Selection(SelectionType type, bool multiSelect)
        {
            Type = type;
            MultiSelect = multiSelect;
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            switch (Type)
            {
                case SelectionType.Dropdown:
                    builder.AppendIndentedLine(indent, $"<select id='{Id}'>");
                    foreach (Option option in Options) Accept(builder, indent == null ? null : $"  {indent}"); 
                    builder.AppendIndentedLine(indent, $"</select>");
                    break;
            }
        }

        public override bool Handle(IAction action)
        {
            throw new NotImplementedException();
        }
    }
}
