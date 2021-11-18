using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Utilities;
using System.Collections;

namespace WebConsoleConnector.Form
{
    public class HtmlForm : ComponentBase, IParent
    {
        public string Title { get; }

        public static IDictionary<Guid, IComponent> Components { get; } = new Dictionary<Guid, IComponent>();

        public IList<IChild> Children { get; } = new List<IChild>();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public HtmlForm(string title, params IChild[] children) : base()
        {
            Title = title;
            foreach (var child in children) Add(child);
        }

        public void Add(IChild child)
        {
            child.Parent = this;
            HtmlForm.Components[child.Id] = child;
            Children.Add(child);
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            builder.AppendIndentedLine("", $"<!DOCTYPE>");
            builder.AppendIndentedLine("", $"<html>");
            builder.AppendIndentedLine("  ", $"<head>");
            builder.AppendIndentedLine("    ", $"<title>{Title}</title>");
            builder.AppendIndentedLine("  ", $"</head>");
            builder.AppendIndentedLine("  ", $"<body>");
            foreach (var child in Children) child.Accept(builder, "    ");
            builder.AppendIndentedLine("  ", $"</body>");
            builder.AppendIndentedLine("", $"</html>");
        }

        public string ToHtml()
        {
            StringBuilder builder = new();
            Accept(builder, "");
            return builder.ToString();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IChild item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IChild[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IChild> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
