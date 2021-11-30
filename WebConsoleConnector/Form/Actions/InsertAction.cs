using System;

namespace WebConsoleConnector.Form.Actions
{
    public class InsertAction : ActionBase
    {
        public string Html { get; set; }

        public InsertAction() { }

        public InsertAction(Guid id, string html) : base(id)
        {
            Html = html;
        } 
    }
}
