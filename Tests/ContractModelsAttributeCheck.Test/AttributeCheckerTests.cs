using ContractModelsAttributeCheck.Test.TestTypes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Xunit;

namespace ContractModelsAttributeCheck.Test
{
    public class AttributeCheckerTests
    {
        private readonly AttributeChecker _attributeChecker = new AttributeChecker();
        private readonly Type[] _attributes = new[] { typeof(JsonPropertyNameAttribute), typeof(JsonIgnoreAttribute) };

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

        [Fact]
        public void All_Properties_Should_Have_Attributes()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithAttributes), _attributes);

            var propertiesWithMissingAttributes = results.Where(w => !w.HasRequiredAttribute);
            propertiesWithMissingAttributes.Should().BeEmpty();
        }

        [Fact]
        public void All_Properties_Should_Have_Missing_Attributes()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithoutAttributes), _attributes);

            var propertiesWithMissingAttributes = results.Where(w => w.HasRequiredAttribute);
            propertiesWithMissingAttributes.Should().BeEmpty();
        }

        [Fact]
        public void All_Properties_Should_Have_One_Attribute()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassSomeMissingAttributes), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(1);
            missingAttributes.First().PropertyName.Should().Be(nameof(TestClassSomeMissingAttributes.MissingAttribute));
        }

        [Fact]
        public void ClassWithEnumListProperty_Should_Not_Add_ListProperties()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithEnumProperty), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(1);
            missingAttributes.First().PropertyName.Should().Be(nameof(TestClassWithEnumProperty.EnumProperty));
        }

        [Fact]
        public void ListOfEnum_Should_Not_Add_ListProperties()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(List<ValuesForEnumeration>), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(0);
        }

        [Fact]
        public void Should_Not_Add_Framework_Types()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(Uri), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(0);
        }

        [Fact]
        public void Array_Should_Not_Add_ArrayProperties()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithAttributes.ClassA[]), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(0);
        }

        [Fact]
        public void Validate_DistinctTypeList()
        {
            var distinctTypeList = new DistinctTypeList();
            distinctTypeList.Add(typeof(TestClassWithoutAttributes.ClassA));
            distinctTypeList.Add(typeof(TestClassWithoutAttributes.ClassB));

            var results = _attributeChecker.CheckPropertiesForAttributes(distinctTypeList, _attributes);
            results.Count.Should().Be(distinctTypeList.GetAllTypes().Sum(s => s.GetProperties().Length));
        }


        public class TestClassSomeMissingAttributes
        {
            [JsonPropertyName("number")]
            public int Number { get; set; }
            [JsonIgnore]
            public string IgnoreMe { get; set; } = string.Empty;
            public string MissingAttribute { get; set; } = string.Empty;
        }
    }
}
