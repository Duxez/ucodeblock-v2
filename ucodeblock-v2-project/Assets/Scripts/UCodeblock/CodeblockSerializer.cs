using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UCodeblock
{
    public class CodeblockSerializer
    {
        public sealed class Settings
        {
            public bool Minify { get; set; } = false;

            internal JsonSerializerSettings ToJsonSettings()
            {
                return new JsonSerializerSettings
                {
                    Formatting = Minify ? Formatting.None : Formatting.Indented
                };
            }
        }

        public Settings SerializerSettings { get; set; } = new Settings();

        public string SerializeCodeblockTree(CodeblockTree tree) 
            => SerializeCodeblockTreeToJObject(tree).ToString(SerializerSettings.ToJsonSettings().Formatting);
        private JObject SerializeCodeblockTreeToJObject(CodeblockTree tree)
        {
            JObject mainChain = SerializeCodeblockChainToJObject(tree.MainChain);
            JArray looseChains = new JArray(tree.LooseChains.Select(looseChain => SerializeCodeblockChainToJObject(looseChain)).ToArray());

            JObject obj = new JObject()
            {
                { "mainChain", mainChain },
                { "looseChains", looseChains }
            };
            return obj;
        }

        public string SerializeCodeblockChain(ExecuteableCodeblockChain chain) 
            => SerializeCodeblockChainToJObject(chain).ToString(SerializerSettings.ToJsonSettings().Formatting);
        private JObject SerializeCodeblockChainToJObject(ExecuteableCodeblockChain chain)
        {
            JArray items = new JArray(chain.Select(b => SerializeCodeblockToJObject(b as Codeblock)).ToArray());

            JObject obj = new JObject
            {
                { "items", items }
            };
            return obj;
        }

        public string SerializeCodeblock(Codeblock block) 
            => SerializeCodeblockToJObject(block).ToString(SerializerSettings.ToJsonSettings().Formatting);
        private JObject SerializeCodeblockToJObject(Codeblock block)
        {
            JToken type = block.GetType().ToString();
            JArray evaluateables = new JArray(block.Arguments.Arguments.Select(a => SerializeCodeblockToJObject(a.Evaluateble as Codeblock)).ToArray());

            JObject obj = new JObject
            {
                { "type", type },
                { "evaluateables", evaluateables }
            };
            return obj;
        }
    }
}
