using System;

namespace UCodeblock
{
    public abstract class Codeblock
    {
        public abstract string Content { get; }
        public virtual ArgumentType[] ArgumentTypes => new ArgumentType[0];

        public CodeblockArguments Arguments { get; set; }
        protected ICodeblockContext Context { get; private set; }

        protected Codeblock()
        {
            Arguments = CodeblockArguments.FromArgumentTypes(ArgumentTypes);
        }
        public static Codeblock Create<T>() where T : Codeblock
        {
            Codeblock block = Activator.CreateInstance<T>();
            return block;
        }

        public void SetContext(ICodeblockContext context)
        {
            Context = context;
        }
        public CodeblockGroupType GetGroupType()
        {
            // TODO: Logic block
            if (this is IExecuteableCodeblock)
                return CodeblockGroupType.Action;
            if (this is IEvaluateableCodeblock)
                return CodeblockGroupType.Value;

            return CodeblockGroupType.Unknown;
        }
    }
}