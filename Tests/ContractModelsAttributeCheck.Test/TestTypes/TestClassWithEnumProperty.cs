using System;
using System.Collections.Generic;
using System.Text;

namespace ContractModelsAttributeCheck.Test.TestTypes
{
    public class TestClassWithEnumProperty
    {
        public List<ValuesForEnum> EnumProperty { get; set; }

    }

    public enum ValuesForEnum
    {
        Yes,
        No,
        Whatever
    }
}
