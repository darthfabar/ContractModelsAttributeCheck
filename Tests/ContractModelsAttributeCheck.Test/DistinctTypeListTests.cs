using ContractModelsAttributeCheck.Test.TestTypes;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace ContractModelsAttributeCheck.Test
{
    public class DistinctTypeListTests
    {
        [Fact]
        public void Test_Add_Same_Type_Multiple_Times()
        {
            var distinctTypes = new DistinctTypeList();
            distinctTypes.Add(typeof(DistinctTypeList));
            distinctTypes.Add(typeof(DistinctTypeList));

            distinctTypes.GetAllTypes().Count().Should().Be(1);
        }

        [Fact]
        public void New_DistinctTypeList_Should_Be_Empty()
        {
            var distinctTypes = new DistinctTypeList();
            distinctTypes.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void AddRange_Should_Contain_Types_Of_Both_Lists()
        {
            var typeList1 = new[] { typeof(TestClassWithAttributes), typeof(TestClassWithAttributes.ClassA) };
            var typeList2 = new[] { typeof(TestClassWithoutAttributes), typeof(TestClassWithoutAttributes.ClassA) };

            var distinctList1 = new DistinctTypeList();
            var distinctList2 = new DistinctTypeList();

            distinctList1.AddRange(typeList1);
            distinctList2.AddRange(typeList2);

            distinctList1.AddRange(distinctList2);

            distinctList1.GetAllTypes().Count().Should().Be(typeList1.Length + typeList2.Length);
        }
    }
}
