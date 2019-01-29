using System;

namespace UCodeblock
{
    [Serializable]
    public class CodeblockExecutionException : CodeblockException
    {
        public CodeblockExecutionException() { }
        public CodeblockExecutionException(string message) : base(message) { }
    }
}