using System;
namespace WebConsoleConnector.Form.Actions
{
    public class UpdateAction : ActionBase
    {
        public string Value { get; set; }

        public UpdateAction() { }

        public UpdateAction(Guid id, string value) : base(id)
        {
            Value = value;
        }
    }
}
