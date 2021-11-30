using System;
using System.Text;
using WebConsoleConnector.Form.Actions;

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

        public IComponent.OnClickHandler OnClick { get; set; } = null;

        public IComponent.OnUpdateHandler OnUpdate { get; set; } = null;

        public abstract void Accept(StringBuilder builder, string indent);

        public virtual void Accept(StringBuilder builder) => Accept(builder, null);

        public abstract bool Handle(IAction action);

    }
}
