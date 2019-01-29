using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace UCodeblock
{
    public class CodeblockArguments : IEnumerable<CodeblockArgument>
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

        public IEnumerator<CodeblockArgument> GetEnumerator() => ((IEnumerable<CodeblockArgument>)_arguments).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
