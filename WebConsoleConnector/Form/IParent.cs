using System.Collections.Generic;

namespace WebConsoleConnector.Form
{
    public interface IParent : IComponent, ICollection<IChild>
    {
        IList<IChild> Children { get; }
    }
}
