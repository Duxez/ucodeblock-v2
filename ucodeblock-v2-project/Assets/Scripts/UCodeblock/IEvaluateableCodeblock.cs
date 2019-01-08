namespace UCodeblock
{
    public interface IEvaluateableCodeblock
    {
        ArgumentType ResultingType { get; }
        Argument Evaluate();
    }
}