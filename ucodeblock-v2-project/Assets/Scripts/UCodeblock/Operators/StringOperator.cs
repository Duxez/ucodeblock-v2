namespace UCodeblock.Operators
{
    public sealed class StringOperator : IComparisonTypeOperator
    {
        public ArgumentType TargetType => ArgumentType.String;

        public int Compare(object l, object r) => l.ToString().CompareTo(r.ToString());
    }
}
