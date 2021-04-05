using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ContractModelsAttributeCheck
{
    /// <summary>
    /// Enables you to get all used types in your controllers
    /// </summary>
    public class ApiContractModelsFinder
    {
        private readonly AttributeChecker _attributeChecker = new AttributeChecker();

        /// <summary>
        /// get a IApiDescriptionGroupCollectionProvider from your dependency injection and use it to query all used types
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <param name="mediaTypesToCheck"></param>
        /// <returns></returns>
        public DistinctTypeList GetUsedContractTypes(IApiDescriptionGroupCollectionProvider apiDescription, string? mediaTypesToCheck = null)
        {
            var apiDescriptions = apiDescription.ApiDescriptionGroups?.Items;
            var usedTypes = new DistinctTypeList();
            foreach (var description in apiDescriptions!)
            {
                usedTypes.AddRange(GetUsedContractTypes(description, mediaTypesToCheck));
            }

            return usedTypes;
        }

        /// <summary>
        /// get a ApiDescriptionGroup from your dependency injection and use it to query all used types
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <param name="mediaTypesToCheck"></param>
        /// <returns></returns>
        public DistinctTypeList GetUsedContractTypes(ApiDescriptionGroup apiDescription, string? mediaTypesToCheck = null)
        {
            var apiDescriptions = apiDescription.Items
                .Where(w => w is not null)
                ;
            var usedTypes = new DistinctTypeList();
            foreach (var description in apiDescriptions!)
            {
                var bodyParameter = description.ParameterDescriptions.Where(IsBodyParameter);
                usedTypes.AddRange(bodyParameter.Select(s => s.Type));
                //TODO option to check for default Asp.net classes
                var responseParameter = description.SupportedResponseTypes.Where(w => w.Type != null && IsCorrectMediaType(mediaTypesToCheck, w));
                usedTypes.AddRange(responseParameter.Select(s => s.Type));
            }

            return usedTypes;
        }

        private bool IsBodyParameter(ApiParameterDescription w)
        {
            return w.Source.IsFromRequest && string.Equals(w.Source.DisplayName, "body", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsCorrectMediaType(string? mediaTypesToCheck, ApiResponseType w)
        {
            return string.IsNullOrEmpty(mediaTypesToCheck) || w.ApiResponseFormats.Select(s => s.MediaType).Contains(mediaTypesToCheck);
        }
    }
}
