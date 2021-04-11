#nullable disable
using System;
using System.Collections.Generic;

namespace ContractModelsAttributeCheck.Test.TestTypes
{
    public class TestClassWithoutAttributes
    {
        public string Id { get; set; }
        public ClassA[] Inner1Array { get; set; }
        public List<ClassB> Inner2List { get; set; }
        public Dictionary<string, ClassC> Dict { get; set; }

        public class ClassA
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public ClassB ClassB { get; set; }
        }

        public class ClassB
        {
            public int MyProperty { get; set; }
        }

        public class ClassC
        {
            public int MyProperty { get; set; }
        }
    }
}
