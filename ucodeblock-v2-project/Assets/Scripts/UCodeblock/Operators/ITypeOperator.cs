namespace UCodeblock
{
    public interface ITypeOperator
    {
        ArgumentType TargetType { get; }
    }

    public interface INumericalTypeOperator : ITypeOperator
    {
        object Add(object l, object r);
        object Subtract(object l, object r);
        object Multiply(object l, object r);
        object Divide(object l, object r);
    }
    public interface IComparisonTypeOperator : ITypeOperator
    {
        int Compare(object l, object r);
    }
    public interface ILogicalTypeOperator : ITypeOperator
    {
        bool And(object l, object r);
        bool Or(object l, object r);
    }
}