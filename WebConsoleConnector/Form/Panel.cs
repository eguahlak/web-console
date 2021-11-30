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

        public Border Border { get; }

        public Width Width { get; }
        
        public IList<IChild> Children { get; } = new List<IChild>();

        public int Count => Children.Count;

        public bool IsReadOnly => false;
        
        public Panel(Border border, Width width)
        {
            Border = border;
            Width = width;
        }
        
        public Panel() : this (false, 0.0) { }

        public Panel(Border border) : this(border, 0.0) { }

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine(indent, $"<div class='Panel{Border}' id='{Id}'>");
            foreach (var child in Children) child.Accept(builder, indent == null ? null : $"  {indent}");
            builder.AppendIndentedLine(indent, $"</div>");
        }

        public void Add(IChild child)
        {
            child.Parent = this;
            HttpForm.Components[child.Id] = child;
            Children.Add(child);
        }

        public void Insert(IChild child)
        {
            Add(child);
            HttpForm.Actions.Add(new InsertAction(Id, child.ToHtml()));
        }

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction formEvent) => false;

    }
}
