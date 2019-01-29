using System;

namespace UCodeblock
{
    public abstract class Codeblock
    {
        public abstract string Content { get; }
        public virtual ArgumentType[] ArgumentTypes => new ArgumentType[0];

        public CodeblockArguments Arguments { get; set; }
        protected ICodeblockContext Context { get; private set; }

        public Codeblock()
        {
            Arguments = CodeblockArguments.FromArgumentTypes(ArgumentTypes);
        }
        public static Codeblock Create<T>() where T : Codeblock
        {
            Codeblock block = Activator.CreateInstance<T>();
            return block;
        }
        public static Codeblock Create(Type type)
        {
            if (!type.IsClass || !type.IsSubclassOf(typeof(Codeblock)))
                throw new CodeblockException($"The type {type.Name} is not a descendant type of Codeblock.");
            if (type.GetConstructor(Type.EmptyTypes) == null)
                throw new CodeblockException($"The type {type.Name} does not contain a default, parameterless constructor.");

            Codeblock block = Activator.CreateInstance(type) as Codeblock;
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