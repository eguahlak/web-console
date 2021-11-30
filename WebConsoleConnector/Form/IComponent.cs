using System;
using System.Text;
using WebConsoleConnector.Form.Actions;

namespace WebConsoleConnector.Form
{
    public interface IComponent
    {
        Guid Id { get; }

        delegate void OnClickHandler(IComponent component);

        delegate void OnUpdateHandler(IComponent component, string newValue);

        void Accept(StringBuilder builder, string indent);

        void Accept(StringBuilder builder) => Accept(builder, null);

        bool Handle(IAction formEvent);

        string ToHtml()
        {
            StringBuilder builder = new();
            Accept(builder, "");
            return builder.ToString();
        }
    }
}
