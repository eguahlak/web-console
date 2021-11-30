using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Form.Actions
{
    public enum StateChange
    {
        Hide, Show, Enable, Disable
    }
    
    public class ChangeAction : ActionBase
    {
        public StateChange Change { get; set; }

        public ChangeAction() { }

        public ChangeAction(Guid id, StateChange change) : base(id)
        {
            Change = change;
        }
    }
}
