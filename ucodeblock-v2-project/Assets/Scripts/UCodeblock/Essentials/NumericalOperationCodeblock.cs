using UCodeblock.Operators;

namespace UCodeblock.Essentials
{
    public class NumericalOperationCodeblock : Codeblock, IEvaluateableCodeblock
    {
        public override string Content => "%0 %1 %2";
        public override ArgumentType[] ArgumentTypes => new ArgumentType[] { ArgumentType.Number, ArgumentType.NumericalOperator, ArgumentType.Number };

        public ArgumentType ResultingType => ArgumentType.Number;

        public Argument Evaluate()
        {
            if (Arguments[1].CanEvaluate)
            {
                INumericalTypeOperator numericalOperator = new NumberOperator();
                NumericalOperator operation = (NumericalOperator)Arguments[1].Evaluateble.Evaluate().Value;

                // Numbers default to 0, if an argument is not given
                object l = Arguments[0].Evaluateble?.Evaluate().Value ?? 0;
                object r = Arguments[2].Evaluateble?.Evaluate().Value ?? 0;

                // If the operation is a division and the divisor is 0, throw a DivideByZeroException
                if (operation == NumericalOperator.Division && (float)r == 0)
                    throw new System.DivideByZeroException();

                object value = EvaluateOperation(l, r, numericalOperator, operation);
                return new Argument(value, ResultingType);
            }
            else
            {
                throw new CodeblockOperatorException("The codeblock arguments do not contain an operation.");
            }
        }

        private object EvaluateOperation(object l, object r, INumericalTypeOperator numericalOperator, NumericalOperator operation)
        {
            switch (operation)
            {
                case NumericalOperator.Addition: return numericalOperator.Add(l, r);
                case NumericalOperator.Subtraction: return numericalOperator.Subtract(l, r);
                case NumericalOperator.Multiplication: return numericalOperator.Multiply(l, r);
                case NumericalOperator.Division: return numericalOperator.Divide(l, r);

                default: throw new CodeblockOperatorException("An invalid operator was passed to the codeblock.");
            }
        }
    }
}
