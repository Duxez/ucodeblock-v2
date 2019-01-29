using System;

namespace UCodeblock
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class CodeblockPropertyAttribute : Attribute
    {
        public string Identifier => _identifier;
        private readonly string _identifier;

        public CodeblockPropertyAttribute(string identifier)
        {
            _identifier = identifier;
        }
    }
}