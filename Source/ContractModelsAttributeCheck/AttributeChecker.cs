using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ContractModelsAttributeCheck
{
    /// <summary>
    /// Helps to check properties for attributes
    /// </summary>
    public class AttributeChecker
    {

        /// <summary>
        /// check all properties of types stored in distinctTypeList
        /// </summary>
        /// <param name="distinctTypeList"></param>
        /// <param name="attributesToCheck"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckPropertiesForAttributes(DistinctTypeList distinctTypeList, Type[] attributesToCheck)
        {
            var results = new List<AttributeCheckResult>();
            foreach (var type in distinctTypeList.GetAllTypes())
            {
                results.AddRange(CheckPropertiesOfTypeForAttributes(type, attributesToCheck));
            }
            return results;
        }

        /// <summary>
        /// Searches recursivly for all property-types of typeToCheck and used types
        /// </summary>
        /// <param name="typeToCheck"></param>
        /// <param name="attributesToCheck"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckPropertiesForAttributes(Type typeToCheck, Type[] attributesToCheck)
        {
            var distinctTypeList = GetTypesRecursivlyFromPublicProperties(typeToCheck);

            var results = new List<AttributeCheckResult>();
            foreach (var type in distinctTypeList.GetAllTypes())
            {
                results.AddRange(CheckPropertiesOfTypeForAttributes(type, attributesToCheck));
            }
            return results;
        }


        /// <summary>
        /// search recursivly all property-types of typesToCheck list
        /// </summary>
        /// <param name="typesToCheck"></param>
        /// <param name="attributesToCheck"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckPropertiesForAttributes(IEnumerable<Type> typesToCheck, Type[] attributesToCheck)
        {
            var distinctTypesToCheck = typesToCheck.Distinct();
            var results = new List<AttributeCheckResult>();
            foreach (var type in distinctTypesToCheck)
            {
                results.AddRange(CheckPropertiesForAttributes(type, attributesToCheck));
            }

            return results;
        }

        /// <summary>
        /// Checks all public properties for attributes
        /// </summary>
        /// <param name="typeToCheck"></param>
        /// <param name="attributesToCheck"></param>
        /// <returns></returns>
        public List<AttributeCheckResult> CheckPropertiesOfTypeForAttributes(Type typeToCheck, Type[] attributesToCheck)
        {
            var results = new List<AttributeCheckResult>();
            if (SkipThisTypeFromAttributeChecks(typeToCheck)) return results;
            var properties = GetPublicProperties(typeToCheck);

            return properties
                .Select(property => ValidateProperty(typeToCheck, attributesToCheck, property))
                .ToList();
        }

        /// <summary>
        /// Creates a DistinctTypeList based on input typeToAnalyse containing all propertytypes
        /// </summary>
        /// <param name="typeToAnalyse"></param>
        /// <param name="distinctTypeList"></param>
        /// <returns></returns>
        public DistinctTypeList GetTypesRecursivlyFromPublicProperties(Type typeToAnalyse, DistinctTypeList? distinctTypeList = null)
        {
            distinctTypeList ??= new DistinctTypeList();

            if (ShouldSkipType(distinctTypeList, typeToAnalyse) ||
                SearchForTypesWhenItIsAnArray(typeToAnalyse, distinctTypeList) ||
                SearchForTypesWhenItIsAGenericType(typeToAnalyse, distinctTypeList))
            {
                return distinctTypeList;
            }
            distinctTypeList.Add(typeToAnalyse);
            var properties = GetPublicProperties(typeToAnalyse);
            foreach (var property in properties)
            {
                SearchPropertyForUsedTypes(distinctTypeList, property);
            }
            return distinctTypeList;
        }

        private AttributeCheckResult ValidateProperty(Type typeToCheck, Type[] attributesToCheck, PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes().Select(s => s.GetType()).ToList();
            var missingAttributes = attributesToCheck.Except(attributes).ToList();
            var hasRequiredAttribute = attributesToCheck.Length > missingAttributes.Count;

            var result = new AttributeCheckResult(typeToCheck, hasRequiredAttribute, property.Name, BuildMessage(missingAttributes, hasRequiredAttribute));

            return result;
        }

        private string BuildMessage(List<Type> missingAttributes, bool hasRequiredAttribute)
        {
            return hasRequiredAttribute ? "Ok" : $"One of this Attributes are missing: {string.Join(", ", missingAttributes.Select(s => s.Name))}";
        }

        private bool SkipThisTypeFromAttributeChecks(Type type)
        {
            return type.IsArray || type.IsGenericType && type.GetInterfaces().Contains(typeof(ICollection));
        }

        private void SearchPropertyForUsedTypes(DistinctTypeList distinctTypeList, PropertyInfo property)
        {
            var currentType = property.PropertyType;
            _ = ShouldSkipType(distinctTypeList, currentType) ||
                SearchForTypesWhenItIsAnArray(currentType, distinctTypeList) ||
                SearchForTypesWhenItIsAGenericType(currentType, distinctTypeList) ||
                SearchForTypesWhenItIsAClass(currentType, distinctTypeList);
        }

        private bool SearchForTypesWhenItIsAnArray(Type currentType, DistinctTypeList distinctTypeList)
        {
            if (!currentType.IsArray) return false;
            var typeOfArry = currentType.GetElementType()!;
            GetTypesRecursivlyFromPublicProperties(typeOfArry, distinctTypeList);
            return true;
        }

        private bool SearchForTypesWhenItIsAGenericType(Type currentType, DistinctTypeList distinctTypeList)
        {
            if (!currentType.IsGenericType) return false;

            foreach (var typeArguments in SelectGenericTypeArguments(distinctTypeList, currentType))
            {
                GetTypesRecursivlyFromPublicProperties(typeArguments, distinctTypeList);
            }
            return true;
        }

        private bool SearchForTypesWhenItIsAClass(Type currentType, DistinctTypeList distinctTypeList)
        {
            if (!currentType.IsClass) return false;
            distinctTypeList.Add(currentType);
            GetTypesRecursivlyFromPublicProperties(currentType, distinctTypeList);
            return true;
        }

        private static IEnumerable<Type> SelectGenericTypeArguments(DistinctTypeList distinctTypeList, Type currentType)
        {
            return currentType.GetGenericArguments().Where(w => !ShouldSkipType(distinctTypeList, w));
        }

        private static PropertyInfo[] GetPublicProperties(Type typeToAnalyse)
        {
            return typeToAnalyse.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        private static bool ShouldSkipType(DistinctTypeList distinctTypeList, Type type)
        {
            return !type.IsClass ||
                    type.IsValueType ||
                    type == typeof(string) ||
                    type == typeof(object) ||
                    distinctTypeList.ContainsType(type);
        }
    }
}
