using System;
using System.Collections.Generic;
using System.Text;

namespace ContractModelsAttributeCheck
{

    /// <summary>
    /// AttributeCheckResult
    /// </summary>
    public class AttributeCheckResult
    {

        /// <summary>
        /// Fullname or type.ToString
        /// </summary>
        public string Fullname { get; } 

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; } 

        /// <summary>
        /// validation message
        /// </summary>
        public string Message { get;} 

        /// <summary>
        /// 
        /// </summary>
        public bool HasRequiredAttribute { get; }

        /// <summary>
        /// Type 
        /// </summary>
        public Type Type { get; } 

        public AttributeCheckResult(Type type, bool hasRequiredAttribute, string propertyname, string message)
        {
            Type = type;
            PropertyName = propertyname;
            HasRequiredAttribute = hasRequiredAttribute;
            Message = message;
            Fullname = type.FullName ?? type.ToString();
        }
    }
}
