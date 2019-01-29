using System;

namespace UCodeblock
{
    [Serializable]
    public class CodeblockOperatorException : CodeblockException
    {
        public CodeblockOperatorException() { }
        public CodeblockOperatorException(string message) : base(message) { }
    }
}