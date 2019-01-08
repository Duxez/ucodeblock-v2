using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Evaluateble = evaluateable;
        }
    }
}
