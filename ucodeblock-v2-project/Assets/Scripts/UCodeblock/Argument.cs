namespace UCodeblock
{
    public struct Argument
    {
        public ArgumentType Type { get; }
        public object Value { get; set; }

        public Argument(object value)
        {
            Value = value;
            Type = ArgumentTypeHelper.FromObject(value);
        }
        public Argument(object value, ArgumentType type)
        {
            Value = value;
            Type = type;
        }
    }
}