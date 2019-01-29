using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCodeblock.Tests
{
    /// <summary>
    /// A dummy codeblock, to execute an action.
    /// </summary>
    internal class ActionCodeblock : Codeblock, IExecuteableCodeblock
    {
        public override string Content => "Basic Action Codeblock";

        public Action Action { get; set; }
        
        public ActionCodeblock() { }
        public ActionCodeblock(Action action)
        {
            Action = action;
        }

        public void Execute()
        {
            Action?.Invoke();
        }
    }
    internal class LogCodeblock : Codeblock, IExecuteableCodeblock
    {
        public override string Content => "Print %0";
        public override ArgumentType[] ArgumentTypes => new ArgumentType[1] { ArgumentType.String };

        public LogCodeblock() { }

        public void Execute()
        {
            UnityEngine.Debug.Log(Arguments[0].Evaluateble.Evaluate().Value.ToString());
        }
    }
    /// <summary>
    /// A dummy codeblock, to be evaluated to a constant value.
    /// </summary>
    internal class ConstantEvaluateable : Codeblock, IEvaluateableCodeblock
    {
        public override string Content => "Basic Constant Evaluateable";
        public ArgumentType ResultingType => ArgumentType.Unknown;
        
        [CodeblockProperty("value")]
        public object Value { get; set; }

        public ConstantEvaluateable() { }
        public ConstantEvaluateable(object value)
        {
            Value = value;
        }

        public Argument Evaluate() => new Argument(Value, ArgumentTypeHelper.FromObject(Value));
    }
}
