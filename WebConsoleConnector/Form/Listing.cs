using System;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;

namespace WebConsoleConnector.Form
{
    public class Listing : ChildBase
    {
        public List<Item> Items { get; } = new();

        public Listing()
        {
        }

        public override void Accept(StringBuilder builder, string indent)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction formEvent)
        {
            throw new NotImplementedException();
        }

        public class Item : ChildBase
        {
            public override void Accept(StringBuilder builder, string indent)
            {
                throw new NotImplementedException();
            }

            public override bool Handle(IAction formEvent)
            {
                throw new NotImplementedException();
            }
        }
    }
}
