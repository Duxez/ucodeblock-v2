namespace UCodeblock.Operators
{
    public enum NumericalOperator
    {
        [OperatorDisplay("+")]  Addition,
        [OperatorDisplay("-")]  Subtraction,
        [OperatorDisplay("*")]  Multiplication,
        [OperatorDisplay("/")]  Division
    }

    public enum ComparisonOperator
    {
        [OperatorDisplay("==")]  Equal,
        [OperatorDisplay("!=")]  NotEqual,
        [OperatorDisplay("<") ]  LessThan,
        [OperatorDisplay("<=")]  LessThanOrEqual,
        [OperatorDisplay(">") ]  GreaterThan,
        [OperatorDisplay(">=")]  GreaterThanOrEqual
    }

    public enum LogicalOperator
    {
        [OperatorDisplay("&&")]  And,
        [OperatorDisplay("||")]  Or
    }
}