using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Newline : ChildBase
    {
        public static readonly Newline Break = new(false);
        public static readonly Newline Ruler = new(true);
        
        public Border Border { get; }

        public override IParent Parent { get => null; set { } }

        public Newline(Border border)
        {
            Border = border;
        }



        public override void Accept(StringBuilder builder, string indent)
        {
            if (Border) builder.AppendIndentedLine(indent, "<div class='LineStart'></div><hr/>");
            else builder.AppendIndentedLine(indent, "<div class='LineStart'></div><br/>");
        }

        public override bool Handle(IAction action) => false;
    }
}
