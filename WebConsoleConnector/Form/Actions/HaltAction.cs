using System;
namespace WebConsoleConnector.Form.Actions
{
    public class HaltAction : ActionBase
    {
        public string Reason { get; set; }

        public HaltAction() { }

        public HaltAction(Guid id, string reason) : base(id)
        {
            Reason = reason;
        }
    }
}
