using ContractModelsAttributeCheck;
using Xunit;

namespace ContractModelsAttributeCheck.Test
{
    public class Class1Test
    {
        [Fact]
        public void Given_When_Then()
        {
            var class1 = new Class1();

            Assert.NotNull(class1);
        }
    }
}
