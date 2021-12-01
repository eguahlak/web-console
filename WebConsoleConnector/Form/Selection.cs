using System;
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

        public override void Accept(StringBuilder builder, string indent)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction action)
        {
            throw new NotImplementedException();
        }
    }

    public class Selection : ChildBase
    {
        public SelectionType Type { get; }
        public bool MultiSelect { get; }

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
                    builder.AppendIndentedLine(indent, $"<selection id='{Id}'>");
                    builder.AppendIndentedLine(indent, $"</selection>");
                    break;
            }
        }

        public override bool Handle(IAction action)
        {
            throw new NotImplementedException();
        }
    }
}
