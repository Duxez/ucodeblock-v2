using System;

namespace UCodeblock.Operators
{
    public sealed class NumberOperator : INumericalTypeOperator, IComparisonTypeOperator
    {
        public ArgumentType TargetType => ArgumentType.Number;

        public object Add(object l, object r) => Convert.ToSingle(l) + Convert.ToSingle(r);
        public object Subtract(object l, object r) => Convert.ToSingle(l) - Convert.ToSingle(r);
        public object Multiply(object l, object r) => Convert.ToSingle(l) * Convert.ToSingle(r);
        public object Divide(object l, object r) => Convert.ToSingle(l) / Convert.ToSingle(r);

        public int Compare(object l, object r) => Convert.ToSingle(l).CompareTo(Convert.ToSingle(r));
    }
}
