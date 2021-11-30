using System;
using System.Text;
using WebConsoleConnector.Form.Actions;

namespace WebConsoleConnector.Form
{
    public abstract class ChildBase : ComponentBase, IChild
    {
        private IParent parent = null;

        private bool hidden = false;
        private bool disabled = false;

        public bool Hidden
        {
            get => hidden;
            set
            {
                if (hidden != value)
                {
                    hidden = value;
                    if (parent == null) return;
                    if (hidden) HttpForm.Actions.Add(new ChangeAction(Id, StateChange.Hide));
                    else HttpForm.Actions.Add(new ChangeAction(Id, StateChange.Show));
                }
            }
        }

        public bool Disabled
        {
            get => disabled;
            set
            {
                if (disabled != value)
                {
                    disabled = value;
                    if (parent == null) return;
                    if (disabled) HttpForm.Actions.Add(new ChangeAction(Id, StateChange.Disable));
                    else HttpForm.Actions.Add(new ChangeAction(Id, StateChange.Enable));
                }
            }
        }

        public string State => $"{(hidden ? "hidden " : "")}{(disabled ? "disabled " : "")}";

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
