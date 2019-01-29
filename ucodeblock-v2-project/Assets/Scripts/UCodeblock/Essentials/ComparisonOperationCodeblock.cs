using UCodeblock.Operators;

namespace UCodeblock.Essentials
{
    public class ComparisonOperationCodeblock : Codeblock, IEvaluateableCodeblock
    {
        public override string Content => "%0 %1 %2";
        public override ArgumentType[] ArgumentTypes => new ArgumentType[] { _validLogicalTypes, ArgumentType.ComparisonOperator, _validLogicalTypes };

        public ArgumentType ResultingType => ArgumentType.Boolean;

        private const ArgumentType _validLogicalTypes = ArgumentType.Number | ArgumentType.String | ArgumentType.Boolean;

        public Argument Evaluate()
        {
            if (!Arguments[0].CanEvaluate || !Arguments[2].CanEvaluate)
                throw new CodeblockExecutionException("The codeblock arguments can not be empty.");

            object l = Arguments[0].Evaluateble.Evaluate().Value;
            object r = Arguments[2].Evaluateble.Evaluate().Value;

            // Get the types of the arguments
            ArgumentType lType = ArgumentTypeHelper.FromObject(l);
            ArgumentType rType = ArgumentTypeHelper.FromObject(r);

            // Ensure the arguments don't have different types
            if (lType != rType)
                throw new CodeblockExecutionException("The codeblock arguments must have the same type.");

            // Use the left argument to see which type the comparison should take
            IComparisonTypeOperator comparisonOperator;
            ComparisonOperator operation = (ComparisonOperator)Arguments[1].Evaluateble.Evaluate().Value;
            switch (lType)
            {
                case ArgumentType.Number: comparisonOperator = new NumberOperator(); break;
                case ArgumentType.String: comparisonOperator = new StringOperator(); break;
                case ArgumentType.Boolean: comparisonOperator = new BooleanOperator(); break;

                default: throw new CodeblockExecutionException($"No valid operator found for argument type {lType}.");
            }

            object result = EvaluateOperation(l, r, comparisonOperator, operation);

            return new Argument(result, ResultingType);
        }

        private object EvaluateOperation(object l, object r, IComparisonTypeOperator comparisonOperator, ComparisonOperator operation)
        {
            int comparisonResult = comparisonOperator.Compare(l, r);
            switch (operation)
            {
                case ComparisonOperator.Equal: return comparisonResult == 0;
                case ComparisonOperator.NotEqual: return comparisonResult != 0;
                case ComparisonOperator.LessThan: return comparisonResult < 0;
                case ComparisonOperator.LessThanOrEqual: return comparisonResult <= 0;
                case ComparisonOperator.GreaterThan: return comparisonResult > 0;
                case ComparisonOperator.GreaterThanOrEqual: return comparisonResult >= 0;

                default: throw new CodeblockExecutionException("An invalid operator was passed to the codeblock.");
            }
        }
    }
}
