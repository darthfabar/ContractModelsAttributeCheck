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
        /// true if at least one attribute is used
        /// </summary>
        public bool HasRequiredAttribute { get; }

        /// <summary>
        /// Type 
        /// </summary>
        public Type Type { get; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="hasRequiredAttribute"></param>
        /// <param name="propertyname"></param>
        /// <param name="message"></param>
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
