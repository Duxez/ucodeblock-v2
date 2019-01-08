using System;

namespace UCodeblock
{
    [Flags]
    public enum ArgumentType
    {
        Unknown = 0,
        Number = 1,
        String = 2,
        Boolean = 4,

        NumericalOperator = 8,
        ComparisonOperator = 16,
        LogicalOperator = 32
    }
}
