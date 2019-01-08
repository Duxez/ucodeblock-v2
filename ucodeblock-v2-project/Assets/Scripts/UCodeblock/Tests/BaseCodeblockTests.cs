using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using UCodeblock;
using UCodeblock.Operators;
using UCodeblock.Essentials;

namespace UCodeblock.Tests
{
    public class BaseCodeblockTests
    {
        /// <summary>
        /// A dummy codeblock, to execute an action.
        /// </summary>
        class ActionCodeblock : Codeblock, IExecuteableCodeblock
        {
            public override string Content => "Basic Action Codeblock";

            private Action _action;

            public ActionCodeblock(Action action)
            {
                _action = action;
            }

            public void Execute()
            {
                _action?.Invoke();
            }
        }
        /// <summary>
        /// A dummy codeblock, to be evaluated to a constant value.
        /// </summary>
        class ConstantEvaluateable : Codeblock, IEvaluateableCodeblock
        {
            public override string Content => "Basic Constant Evaluateable";
            public ArgumentType ResultingType => ArgumentType.Unknown;

            private object _content;

            public ConstantEvaluateable(object content)
            {
                _content = content;
            }

            public Argument Evaluate() => new Argument(_content, ArgumentTypeHelper.FromObject(_content));
        }

        /// <summary>
        /// Attempts to execute a single codeblock.
        /// </summary>
        [Test]
        public void SingularExecute()
        {
            int x = 0;

            ActionCodeblock actionCodeblock = new ActionCodeblock(() => x = 1);
            actionCodeblock.Execute();

            Assert.AreEqual(x, 1);
        }

        /// <summary>
        /// Attempts to execute a tree of codeblocks.
        /// </summary>
        [Test]
        public void TreeExecute()
        {
            int x = 0;

            CodeblockTree tree = new CodeblockTree();
            tree.MainChain.Add(new ActionCodeblock(() => x += 1));
            tree.MainChain.Add(new ActionCodeblock(() => x += 2));

            tree.Execute();

            Assert.AreEqual(x, 3);
        }

        /// <summary>
        /// Attempts to perform various numerical operations on the codeblocks.
        /// </summary>
        [Test]
        public void NumericalOperations()
        {
            NumericalOperationCodeblock block = new NumericalOperationCodeblock();
            block.Arguments[0].SetEvaluateable(new ConstantEvaluateable(4));
            block.Arguments[2].SetEvaluateable(new ConstantEvaluateable(3));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(NumericalOperator.Addition));
            Assert.AreEqual(Convert.ToInt32(block.Evaluate().Value), 7);

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(NumericalOperator.Multiplication));
            Assert.AreEqual(Convert.ToInt32(block.Evaluate().Value), 12);

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(NumericalOperator.Subtraction));
            Assert.AreEqual(Convert.ToInt32(block.Evaluate().Value), 1);
        }

        /// <summary>
        /// Attempts to perform various logical operations on the codeblocks.
        /// </summary>
        [Test]
        public void ComparisonOperations()
        {
            ComparisonOperationCodeblock block = new ComparisonOperationCodeblock();
            block.Arguments[0].SetEvaluateable(new ConstantEvaluateable(4));
            block.Arguments[2].SetEvaluateable(new ConstantEvaluateable(3));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(ComparisonOperator.Equal));
            Assert.False(Convert.ToBoolean(block.Evaluate().Value));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(ComparisonOperator.GreaterThanOrEqual));
            Assert.True(Convert.ToBoolean(block.Evaluate().Value));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(ComparisonOperator.LessThan));
            Assert.False(Convert.ToBoolean(block.Evaluate().Value));
        }

        [Test]
        public void LogicalOperations()
        {
            LogicalOperationCodeblock block = new LogicalOperationCodeblock();
            block.Arguments[0].SetEvaluateable(new ConstantEvaluateable(true));
            block.Arguments[2].SetEvaluateable(new ConstantEvaluateable(false));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(LogicalOperator.And));
            Assert.False(Convert.ToBoolean(block.Evaluate().Value));

            block.Arguments[1].SetEvaluateable(new ConstantEvaluateable(LogicalOperator.Or));
            Assert.True(Convert.ToBoolean(block.Evaluate().Value));
        }

        /// <summary>
        /// Attempts to automatically find the type of a codeblock.
        /// </summary>
        [Test]
        public void CodeblockGroupTypes()
        {
            Assert.AreEqual(new ActionCodeblock(() => { }).GetGroupType(), CodeblockGroupType.Action);
            Assert.AreEqual(new ConstantEvaluateable(0).GetGroupType(), CodeblockGroupType.Value);
        }
    }
}