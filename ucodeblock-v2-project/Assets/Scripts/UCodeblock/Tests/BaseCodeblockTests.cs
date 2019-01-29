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
    internal class BaseCodeblockTests
    {
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