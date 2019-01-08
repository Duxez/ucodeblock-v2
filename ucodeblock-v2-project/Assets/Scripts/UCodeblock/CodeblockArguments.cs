using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCodeblock
{
    public class CodeblockArguments
    {
        public static CodeblockArguments Empty => new CodeblockArguments(new CodeblockArgument[0]);

        public CodeblockArgument this[int i] => _arguments[i];
        public CodeblockArgument[] Arguments => _arguments;

        private CodeblockArgument[] _arguments;

        private CodeblockArguments(CodeblockArgument[] arguments)
        {
            _arguments = arguments;
        }

        public static CodeblockArguments FromArgumentTypes(params ArgumentType[] argTypes)
            => new CodeblockArguments(argTypes.Select(a => new CodeblockArgument(a)).ToArray());
    }
}
