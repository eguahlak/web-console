using System;
using System.Collections;
using System.Collections.Generic;

namespace WebConsoleConnector.Form
{
    public interface IParent : IComponent, ICollection<IChild>
    {
        IList<IChild> Children { get; }

        void ICollection<IChild>.Clear() => Children.Clear();

        bool ICollection<IChild>.Contains(IChild item) => Children.Contains(item);

        void ICollection<IChild>.CopyTo(IChild[] array, int arrayIndex) =>
            Children.CopyTo(array, arrayIndex);

        IEnumerator<IChild> IEnumerable<IChild>.GetEnumerator() =>
            Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();
     }
}
