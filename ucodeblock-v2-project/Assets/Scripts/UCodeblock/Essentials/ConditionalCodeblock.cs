using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCodeblock.Essentials
{
    public class ConditionalCodeblock : Codeblock, IExecuteableCodeblock
    {
        public override string Content => "If %0 do";
        public override ArgumentType[] ArgumentTypes => new ArgumentType[1] { ArgumentType.Boolean };

        [CodeblockProperty("body")]
        public ExecuteableCodeblockChain Body { get; set; }

        public ConditionalCodeblock()
        {
            Body = new ExecuteableCodeblockChain();
        }

        public void Execute()
        {
            if (!Arguments[0].CanEvaluate)
                throw new CodeblockExecutionException("The condition may not be empty.");

            if ((bool)Arguments[0].Evaluateble.Evaluate().Value)
            {
                foreach (IExecuteableCodeblock codeblock in Body)
                {
                    codeblock.Execute();
                }
            }
        }
    }
}
