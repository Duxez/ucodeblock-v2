using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

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
        public Type[] BlockTypes => _codeblockTypes.ToArray();

        private List<Type> _codeblockTypes;

        private static readonly Type _defaultBlockType = typeof(Codeblock);
        private static readonly BindingFlags _codeblockPropertyBindings = BindingFlags.Public | BindingFlags.Instance;

        public CodeblockSerializer()
        {
            _codeblockTypes = new List<Type>();
            AppendAssembly(Assembly.GetExecutingAssembly());
        }
        public CodeblockSerializer(params Assembly[] assemblies)
        {
            _codeblockTypes = new List<Type>();
            AppendAssemblies(assemblies);
        }

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
            if (block == null)
                return new JObject();

            Type blockType = block.GetType();

            JObject obj = new JObject
            {
                { "type", blockType.Name }
            };

            JObject[] arguments = block.Arguments.Select(a => SerializeCodeblockToJObject(a.Evaluateble as Codeblock)).ToArray();
            if (arguments != null && arguments.Length > 0)
            {
                obj.Add("arguments", new JArray(arguments));
            }

            // TODO: Maybe cleanup codeblock property serialization
            IEnumerable<PropertyInfo> codeblockProperties = blockType
                .GetProperties(_codeblockPropertyBindings)
                .Where(p => p.CanRead && p.GetCustomAttribute<CodeblockPropertyAttribute>() != null);
            foreach (PropertyInfo property in codeblockProperties)
            {
                string identifier = property.GetCustomAttribute<CodeblockPropertyAttribute>()?.Identifier ?? property.Name;
                object value = property.GetValue(block);

                if (value != null)
                {
                    JToken token = null;

                    Type propertyType = property.PropertyType;
                    if (propertyType == typeof(CodeblockTree)) token = SerializeCodeblockTreeToJObject(value as CodeblockTree);
                    else if (propertyType == typeof(ExecuteableCodeblockChain)) token = SerializeCodeblockChainToJObject(value as ExecuteableCodeblockChain);
                    else if (propertyType == typeof(Codeblock)) token = SerializeCodeblockToJObject(value as Codeblock);
                    else token = JToken.FromObject(value);

                    obj.Add(identifier, token);
                }
            }
            
            return obj;
        }


        public CodeblockTree DeserializeCodeblockTree(string json)
        {
            JObject treeObject = JObject.Parse(json);

            ExecuteableCodeblockChain mainChain = DeserializeCodeblockChain(treeObject["mainChain"].ToString());
            List<ExecuteableCodeblockChain> looseChains = treeObject["looseChains"].Select(c => DeserializeCodeblockChain(c.ToString())).ToList();

            CodeblockTree codeblockTree = new CodeblockTree
            {
                MainChain = mainChain,
                LooseChains = looseChains
            };
            return codeblockTree;
        }

        public ExecuteableCodeblockChain DeserializeCodeblockChain(string json)
        {
            JObject chainObject = JObject.Parse(json);

            IEnumerable<IExecuteableCodeblock> items = chainObject["items"].Select(t => DeserializeCodeblock(t.ToString()) as IExecuteableCodeblock);

            ExecuteableCodeblockChain chain = new ExecuteableCodeblockChain(items);
            return chain;
        }

        public Codeblock DeserializeCodeblock(string json)
        {
            JObject codeblockObject = JObject.Parse(json);

            string typeName = codeblockObject["type"].ToString();
            Type blockType = _codeblockTypes.FirstOrDefault(t => t.Name == typeName);
            if (blockType == null)
                throw new CodeblockException($"The type {typeName} was not found in the registered assemblies.");

            Codeblock block = Codeblock.Create(blockType);

            JArray argumentArray = codeblockObject["arguments"] as JArray;
            if (argumentArray != null && argumentArray.Count > 0)
            {
                IEvaluateableCodeblock[] arguments = argumentArray
                    .Values<JObject>()
                    .Select(j => DeserializeCodeblock(j.ToString()) as IEvaluateableCodeblock)
                    .ToArray();

                for (int i = 0; i < arguments.Length; i++)
                    block.Arguments[i].SetEvaluateable(arguments[i]);
            }

            // TODO: Maybe cleanup codeblock property serialization
            IEnumerable<PropertyInfo> codeblockProperties = blockType
                .GetProperties(_codeblockPropertyBindings)
                .Where(p => p.CanRead && p.GetCustomAttribute<CodeblockPropertyAttribute>() != null);
            foreach (PropertyInfo property in codeblockProperties)
            {
                string identifier = property.GetCustomAttribute<CodeblockPropertyAttribute>()?.Identifier ?? property.Name;

                JProperty valueProperty = codeblockObject.Property(identifier);
                if (valueProperty != null)
                {
                    object value = null;

                    Type propertyType = property.PropertyType;
                    if (propertyType == typeof(CodeblockTree)) value = DeserializeCodeblockTree(valueProperty.Value.ToString());
                    else if (propertyType == typeof(ExecuteableCodeblockChain)) value = DeserializeCodeblockChain(valueProperty.Value.ToString());
                    else if (propertyType == typeof(Codeblock)) value = DeserializeCodeblock(valueProperty.Value.ToString());
                    else value = valueProperty.Value.ToObject(property.PropertyType);

                    property.SetValue(block, value);
                }
            }

            return block;
        }


        public void AppendAssembly(Assembly assembly)
        {
            IEnumerable<Type> types = FetchCodeblockTypesFromAssembly(assembly);
            AppendTypes(types);
        }
        public void AppendAssemblies(params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies
                .SelectMany(a => FetchCodeblockTypesFromAssembly(a));
            AppendTypes(types);
        }
        private void AppendTypes(IEnumerable<Type> types)
        {
            _codeblockTypes.AddRange(types);
        }

        private static IEnumerable<Type> FetchCodeblockTypesFromAssembly(Assembly assembly)
            => assembly.GetTypes().Where(t => t.IsSubclassOf(_defaultBlockType) && !t.IsAbstract);
    }
}
