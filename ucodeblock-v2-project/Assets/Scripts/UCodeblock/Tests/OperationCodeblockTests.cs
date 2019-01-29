using System;
using System.Collections;

using UnityEngine;
using UnityEngine.TestTools;

using NUnit.Framework;

using UCodeblock;
using UCodeblock.Operators;
using UCodeblock.Essentials;

namespace UCodeblock.Tests
{
    internal class OperationCodeblockTests
    {
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
    }
}