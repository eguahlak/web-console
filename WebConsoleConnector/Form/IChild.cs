using System;
namespace WebConsoleConnector.Form
{
    public interface IChild : IComponent
    {
        IParent Parent { get; set; }
    }
}
