using System;

namespace UCodeblock.Operators
{
    public sealed class BooleanOperator : IComparisonTypeOperator, ILogicalTypeOperator
    {
        public ArgumentType TargetType => ArgumentType.Boolean;

        public int Compare(object l, object r) => Convert.ToBoolean(l).CompareTo(Convert.ToBoolean(r));

        public bool And(object l, object r) => Convert.ToBoolean(l) && Convert.ToBoolean(r);
        public bool Or(object l, object r) => Convert.ToBoolean(l) || Convert.ToBoolean(r);
    }
}
