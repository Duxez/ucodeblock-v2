using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCodeblock.Log
{
    internal class ConsoleLogger : ILogger
    {
        public void Write(object obj)
        {
            Debug.Write(obj);
        }

        public void WriteLine(object obj)
        {
            WriteLine(obj.ToString() + Environment.NewLine);
        }
    }
}
