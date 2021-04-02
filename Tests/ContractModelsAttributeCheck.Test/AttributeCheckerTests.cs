using ContractModelsAttributeCheck.Test.TestTypes;
using FluentAssertions;
using System;
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

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute);
            missingAttributes.Should().BeEmpty();
        }

        [Fact]
        public void All_Properties_Should_Have_Missing_Attributes()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithoutAttributes), _attributes);

            var propertiesWithAttributes = results.Where(w => w.HasRequiredAttribute);
            propertiesWithAttributes.Should().BeEmpty();
        }

        [Fact]
        public void All_Properties_Should_Have_One_Attribute()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassSomeMissingAttributes), _attributes);

            var missingAttributes = results.Where(w => !w.HasRequiredAttribute).ToList();
            missingAttributes.Count.Should().Be(1);
            missingAttributes.First().PropertyName.Should().Be(nameof(TestClassSomeMissingAttributes.MissingAttribute));
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
