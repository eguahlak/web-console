using System;
namespace WebConsoleConnector.Form
{
    public interface IChild : IComponent
    {
        IParent Parent { get; set; }
        bool Hidden { get; set; }
        bool Disabled { get; set; }
    }
}
