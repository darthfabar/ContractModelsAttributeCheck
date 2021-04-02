#nullable disable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContractModelsAttributeCheck.Test.TestTypes
{
    public class TestClassWithAttributes
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("inner")]
        public ClassA[] Inner1Array { get; set; }
        [JsonPropertyName("inner2")]
        public List<ClassB> Inner2List { get; set; }

        [JsonPropertyName("dict")]
        public Dictionary<string, ClassC> Dict { get; set; }

        public class ClassA
        {
            [JsonPropertyName("Number")]
            public int Number { get; set; }
            [JsonPropertyName("Text")]
            public string Text { get; set; }
            [JsonPropertyName("ClassB")]
            public ClassB ClassB { get; set; }
        }

        public class ClassB
        {
            [JsonPropertyName("MyProperty")]
            public int MyProperty { get; set; }
        }

        public class ClassC
        {
            [JsonPropertyName("MyProperty")]
            public int MyProperty { get; set; }
        }
    }
}
