using System;
using System.Text;

namespace WebConsoleConnector.Form
{
    public abstract class ChildBase : ComponentBase, IChild
    {
        private IParent parent = null;

        private bool hidden = false;
        private bool enabled = true;

        //public void Hide

        public virtual IParent Parent
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
