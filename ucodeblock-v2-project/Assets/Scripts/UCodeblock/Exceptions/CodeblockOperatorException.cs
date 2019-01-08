using System;

namespace UCodeblock
{
    [Serializable]
    public class CodeblockOperatorException : Exception
    {
        public CodeblockOperatorException() { }
        public CodeblockOperatorException(string message) : base(message) { }
    }
}