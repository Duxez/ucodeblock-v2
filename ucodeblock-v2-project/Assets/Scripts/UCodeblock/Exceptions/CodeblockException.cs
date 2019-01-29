using System;

namespace UCodeblock
{
    [Serializable]
    public class CodeblockException : Exception
    {
        public CodeblockException() { }
        public CodeblockException(string message) : base(message) { }
    }
}