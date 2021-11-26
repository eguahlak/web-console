using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WebConsoleConnector.Form.Actions;

namespace WebConsoleConnector.Form
{
    public class Table: ChildBase, IParent
    {
        public Table()
        {
        }

        public IList<IChild> Children => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public override void Accept(StringBuilder builder, string indent)
        {
            throw new NotImplementedException();
        }

        public void Add(IChild item)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(IAction formEvent)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IChild item)
        {
            throw new NotImplementedException();
        }

    }
}
