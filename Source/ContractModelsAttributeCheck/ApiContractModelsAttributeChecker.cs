using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractModelsAttributeCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiContractModelsAttributeChecker
    {
        private readonly AttributeChecker _attributeChecker = new AttributeChecker();
        private readonly ApiContractModelsFinder _apiContractModelsFinder = new ApiContractModelsFinder();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <param name="attributesToCheck"></param>
        /// <param name="mediaTypesToCheck"></param>
        /// <param name="typesToIgnore"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckAttributesOfApiContractTypes(ApiDescriptionGroup apiDescription, Type[] attributesToCheck, string? mediaTypesToCheck = null, IEnumerable<Type>? typesToIgnore = null)
        {
            var typeList = _apiContractModelsFinder.GetUsedContractTypes(apiDescription, mediaTypesToCheck);
            return CheckTypeAttributes(typeList, attributesToCheck, typesToIgnore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <param name="attributesToCheck"></param>
        /// <param name="mediaTypesToCheck"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckAttributesOfApiContractTypes(IApiDescriptionGroupCollectionProvider apiDescription, Type[] attributesToCheck, string? mediaTypesToCheck = null, IEnumerable<Type>? typesToIgnore = null)
        {
            var typeList = _apiContractModelsFinder.GetUsedContractTypes(apiDescription, mediaTypesToCheck);
            return CheckTypeAttributes(typeList, attributesToCheck, typesToIgnore);
        }

        private List<AttributeCheckResult> CheckTypeAttributes(DistinctTypeList typeList, Type[] attributesToCheck, IEnumerable<Type>? typesToIgnore = null)
        {
            var filteredTypeList = typeList.GetAllTypes().Where(w => typesToIgnore == null || !typesToIgnore.Contains(w));
            return _attributeChecker.CheckPropertiesForAttributes(filteredTypeList, attributesToCheck);
        }
    }
}
