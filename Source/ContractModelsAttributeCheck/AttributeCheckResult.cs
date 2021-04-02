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
        public string Fullname { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// validation message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool HasRequiredAttribute { get; set; } 
    }
}
