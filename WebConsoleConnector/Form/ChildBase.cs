using System;
using System.Text;

namespace WebConsoleConnector.Form
{
    public abstract class ChildBase : ComponentBase, IChild
    {
        private IParent parent = null;

        public IParent Parent
        {
            get => parent;
            set
            {
                if (parent != null) throw new Exception("Cannot realocate parent");
                if (value == null) return;
                parent = value;
            }
        }

        public ChildBase() { }

    }
}
