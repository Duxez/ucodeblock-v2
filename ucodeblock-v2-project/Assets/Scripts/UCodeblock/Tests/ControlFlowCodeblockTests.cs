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
    internal class ControlFlowCodeblockTests
    {
        [Test]
        public void ConditionalCodeblock()
        {
            ActionCodeblock passExpression = new ActionCodeblock(() => Assert.Pass());

            ConstantEvaluateable condition = new ConstantEvaluateable(true);
            ConditionalCodeblock conditional = new ConditionalCodeblock();

            conditional.Arguments[0].SetEvaluateable(condition);
            conditional.Body.Add(passExpression);

            conditional.Execute();

            Assert.Fail("If the conditional block succeeded, this should never run.");
        }
    }
}