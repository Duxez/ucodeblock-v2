using System;

namespace UCodeblock
{
    [Serializable]
    public class CodeblockExecutionException : Exception
    {
        public CodeblockExecutionException() { }
        public CodeblockExecutionException(string message) : base(message) { }
    }
}