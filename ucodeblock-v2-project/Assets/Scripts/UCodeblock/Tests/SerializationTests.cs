using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TestTools;

using NUnit.Framework;

using UCodeblock;
using UCodeblock.Operators;
using UCodeblock.Essentials;

namespace UCodeblock.Tests
{
    internal class SerializationTests
    {
        private Codeblock GetDummyBlock()
        {
            Codeblock block = new LogCodeblock();
            block.Arguments[0].SetEvaluateable(new ConstantEvaluateable("Test!"));
            return block;
        }
        private ExecuteableCodeblockChain GetDummyChain() => new ExecuteableCodeblockChain(new Codeblock[] { GetDummyBlock() }.Select(b => b as IExecuteableCodeblock));
        private CodeblockTree GetDummyTree() => new CodeblockTree() { MainChain = GetDummyChain(), LooseChains = Enumerable.Repeat(GetDummyChain(), 3).ToList() };

        private static CodeblockSerializer GetSerializer()
        {
            return new CodeblockSerializer(typeof(Codeblock).Assembly, typeof(ActionCodeblock).Assembly)
            {
                SerializerSettings = new CodeblockSerializer.Settings
                {
                    Minify = false
                }
            };
        }

        [Test]
        public void CodeblockSerialization()
        {
            Codeblock dummyBlock = GetDummyBlock();
            CodeblockSerializer serializer = GetSerializer();

            string serialized = serializer.SerializeCodeblock(dummyBlock);
            Codeblock deserialized = serializer.DeserializeCodeblock(serialized);

            Assert.AreEqual(serialized, serializer.SerializeCodeblock(deserialized));
        }

        [Test]
        public void ChainSerialization()
        {
            ExecuteableCodeblockChain dummyChain = GetDummyChain();
            CodeblockSerializer serializer = GetSerializer();

            string serialized = serializer.SerializeCodeblockChain(dummyChain);
            ExecuteableCodeblockChain deserialized = serializer.DeserializeCodeblockChain(serialized);

            Assert.AreEqual(serialized, serializer.SerializeCodeblockChain(deserialized));
        }

        [Test]
        public void TreeSerialization()
        {
            CodeblockTree dummyTree = GetDummyTree();
            CodeblockSerializer serializer = GetSerializer();

            string serialized = serializer.SerializeCodeblockTree(dummyTree);
            CodeblockTree deserialized = serializer.DeserializeCodeblockTree(serialized);

            Assert.AreEqual(serialized, serializer.SerializeCodeblockTree(deserialized));
        }

        [Test]
        public void ConditionalBlockSerialization()
        {
            ActionCodeblock passExpression = new ActionCodeblock(() => Assert.Pass());

            ConstantEvaluateable condition = new ConstantEvaluateable(true);
            ConditionalCodeblock conditional = new ConditionalCodeblock();

            conditional.Arguments[0].SetEvaluateable(condition);
            conditional.Body.Add(passExpression);

            CodeblockSerializer serializer = GetSerializer();

            string serialized = serializer.SerializeCodeblock(conditional);
            Debug.Log(serialized);
            Codeblock deserialized = serializer.DeserializeCodeblock(serialized);

            Assert.AreEqual(serialized, serializer.SerializeCodeblock(deserialized));
        }
    }
}