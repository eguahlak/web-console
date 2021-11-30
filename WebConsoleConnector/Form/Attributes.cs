using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConsoleConnector.Form
{
    public class Border
    {
        private readonly bool value;

        public Border(bool value)
        {
            this.value = value;
        }

        public static implicit operator Border(bool value) => new(value);

        public static implicit operator bool(Border border) => border.value;

        public override string ToString() => value ? " Bordered" : "";
    }

    public class Width
    {
        private readonly double fraction;

        public Width(double fraction)
        {
            this.fraction = fraction;
        }

        public static implicit operator Width(double fraction) => new(fraction);

        public static implicit operator Width(int percent) => new(((double)percent)/100.0);

        public override string ToString() => fraction > 0 ? $" width: {(int)(fraction*100):D}%;" : "";
    
    }
}
