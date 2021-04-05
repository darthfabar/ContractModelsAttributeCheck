using System.Collections.Generic;

namespace ContractModelsAttributeCheck.Test.TestTypes
{
    public class TestClassWithEnumProperty
    {
        public List<ValuesForEnumeration> EnumProperty { get; set; } = new List<ValuesForEnumeration>();

    }

    public enum ValuesForEnumeration
    {
        Yes,
        No,
        Whatever
    }
}
