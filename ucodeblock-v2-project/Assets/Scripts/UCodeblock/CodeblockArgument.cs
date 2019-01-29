using System;

namespace UCodeblock
{
    public class CodeblockArgument
    {
        public ArgumentType Type { get; set; }
        public IEvaluateableCodeblock Evaluateble { get; private set; }

        public bool CanEvaluate => Evaluateble != null;

        public CodeblockArgument(ArgumentType type)
        {
            Type = type;
        }

        public void SetEvaluateable(IEvaluateableCodeblock evaluateable)
        {
            if (AllowEvaluateable(evaluateable))
            {
                Evaluateble = evaluateable;
            }
            else
            {
                throw new ArgumentException($"Invalid IEvaluateableCodeblock Type ({evaluateable.ResultingType.ToString()} passed to CodeblockArgument ({Type.ToString()}");
            }
        }

        public bool AllowEvaluateable(IEvaluateableCodeblock evaluateable)
        {
            if (Type == ArgumentType.Unknown || evaluateable.ResultingType == ArgumentType.Unknown) return true;

            return Type == evaluateable.ResultingType;
        }
    }
}
