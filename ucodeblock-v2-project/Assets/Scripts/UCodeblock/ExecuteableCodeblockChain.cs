using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace UCodeblock
{
    public class ExecuteableCodeblockChain : List<IExecuteableCodeblock>
    {
        public ExecuteableCodeblockChain() : base()
        {
        }
        public ExecuteableCodeblockChain(IEnumerable<IExecuteableCodeblock> blocks) : base(blocks)
        {
        }

        public void TransferToChain(int index, ExecuteableCodeblockChain target, int targetIndex)
        {
            IExecuteableCodeblock item = this[index];

            target.Insert(targetIndex, item);
            this.RemoveAt(index);
        }
        public void TransferToChain(int index, int count, ExecuteableCodeblockChain target, int targetIndex)
        {
            IEnumerable<IExecuteableCodeblock> items = GetRange(index, count);

            target.InsertRange(targetIndex, items);
            this.RemoveRange(index, count);
        }
    }
}