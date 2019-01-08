using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCodeblock
{
    public static class ArgumentTypeHelper
    {
        public static ArgumentType FromObject(object obj)
        {
            if (obj is int || obj is float || obj is double)
                return ArgumentType.Number;
            if (obj is string)
                return ArgumentType.String;
            if (obj is bool)
                return ArgumentType.Boolean;

            return ArgumentType.Unknown;
        }
    }
}
