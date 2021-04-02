using ContractModelsAttributeCheck.Test.TestTypes;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContractModelsAttributeCheck.Test
{
    public class AttributeCheckerTests
    {
        private readonly AttributeChecker _attributeChecker = new AttributeChecker();

        [Fact]
        public void Find_All_Properties_Of_TestClass()
        {
            var typesInTestClass = _attributeChecker.GetTypesRecursivlyFromPublicProperties(typeof(TestClassWithoutAttributes)).GetAllTypes().ToList();
            typesInTestClass.Count.Should().Be(4);
        }

        [Fact]
        public void Find_All_Properties_In_Recursive_Class()
        {
            var typesInTestClass = _attributeChecker.GetTypesRecursivlyFromPublicProperties(typeof(RecursivClass)).GetAllTypes().ToList();
            typesInTestClass.Count.Should().Be(1);
        }



        //public class TestClass
        //{
        //    //public int Id { get; set; }
        //    //public Guid IdGuid { get; set; }
        //    //public string Text { get; set; }
        //    //public object SomeObject { get; set; }

        //    //public string[] ArrayToSkip { get; set; }
        //    //public List<string> ListToSkip { get; set; }
        //    //public int[] Numbers { get; set; }
        //    //public List<int> MoreNumbers { get; set; }
        //    //public InnerTestClass Inner { get; set; }
        //    public InnerTestClass1[] Inner1Array { get; set; }
        //    public List<InnerTestClass2> Inner2List { get; set; }
        //    public Dictionary<string, InnerTestClass> Dict { get; set; }
        //}

        public class InnerTestClass
        {
            public int Id { get; set; }
            public InnerTestClass MyProperty { get; set; }
        }

        public class InnerTestClass1
        {
            public int Id { get; set; }
            public InnerTestClass MyProperty { get; set; }
        }

        public class InnerTestClass2
        {
            public int Id { get; set; }
            public InnerTestClass MyProperty { get; set; }
        }
    }
}
