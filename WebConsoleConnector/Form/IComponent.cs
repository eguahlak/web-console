using System;
using System.Text;

namespace WebConsoleConnector.Form
{
    public interface IComponent
    {
        Guid Id { get; }

        void Accept(StringBuilder builder, string indent);

    }
}
