using System.Linq;
using System.Collections.Generic;

using UCodeblock.Log;

namespace UCodeblock
{
    public class CodeblockTree
    {
        public ExecuteableCodeblockChain MainChain { get; set; }
        public List<ExecuteableCodeblockChain> LooseChains { get; set; }

        internal ILogger Logger { get; }

        public CodeblockTree()
        {
            Logger = new ConsoleLogger();

            MainChain = new ExecuteableCodeblockChain();
            LooseChains = new List<ExecuteableCodeblockChain>();
        }

        public void Execute()
        {
            foreach (IExecuteableCodeblock block in MainChain)
            {
                block.Execute();
            }
        }
    }
}