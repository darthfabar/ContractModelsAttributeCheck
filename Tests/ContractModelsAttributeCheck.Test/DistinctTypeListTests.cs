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
    }
}
