using UCodeblock.Operators;

namespace UCodeblock.Essentials
{
    public class LogicalOperationCodeblock : Codeblock, IEvaluateableCodeblock
    {
        public override string Content => "%0 %1 %2";
        public override ArgumentType[] ArgumentTypes => new ArgumentType[] { ArgumentType.Boolean, ArgumentType.LogicalOperator, ArgumentType.Boolean };

        public ArgumentType ResultingType => ArgumentType.Boolean;

        public Argument Evaluate()
        {
            // If the argument doesn't have a value, false is assumed
            object l = Arguments[0].Evaluateble?.Evaluate().Value ?? false;
            object r = Arguments[2].Evaluateble?.Evaluate().Value ?? true;

            // Use the left argument to see which type the comparison should take
            ILogicalTypeOperator logicalOperator = new BooleanOperator();
            LogicalOperator operation = (LogicalOperator)Arguments[1].Evaluateble.Evaluate().Value;

            object result = EvaluateOperation(l, r, logicalOperator, operation);

            return new Argument(result, ResultingType);
        }

        private object EvaluateOperation(object l, object r, ILogicalTypeOperator logicalOperator, LogicalOperator operation)
        {
            switch (operation)
            {
                case LogicalOperator.And: return logicalOperator.And(l, r);
                case LogicalOperator.Or: return logicalOperator.Or(l, r);

                default: throw new CodeblockExecutionException("An invalid operator was passed to the codeblock.");
            }
        }
    }
}
