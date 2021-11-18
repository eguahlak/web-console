using System;
using System.Text;

namespace WebConsoleConnector.Form
{
    public abstract class ComponentBase : IComponent
    {
        public ComponentBase(Guid id)
        {
            Id = id;
        }

        public ComponentBase() : this (Guid.NewGuid()) { }

        public Guid Id { get; }

        public abstract void Accept(StringBuilder builder, string indent);
    }
}
