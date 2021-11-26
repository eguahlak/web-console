using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;
using WebConsoleConnector.Utilities;

namespace WebConsoleConnector.Form
{
    public class Panel : ChildBase, IParent
    {
        public Panel()
        {
        }

        public IList<IChild> Children { get; } = new List<IChild>();

        public int Count => Children.Count;

        public bool IsReadOnly => false;

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine(indent, $"<div id='{Id}'>");
            foreach (var child in Children) child.Accept(builder, $"{indent}  ");
            builder.AppendIndentedLine(indent, $"</div>");
        }

        public void Add(IChild child)
        {
            child.Parent = this;
            HttpForm.Components[child.Id] = child;
            Children.Add(child);
        }

        public override bool Handle(IAction formEvent) => false;

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
