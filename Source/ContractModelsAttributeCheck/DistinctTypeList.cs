
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ContractModelsAttributeCheck
{
    /// <summary>
    /// Ensures that each element in the list is unique
    /// </summary>
    public class DistinctTypeList
    {
        private readonly HashSet<Type> _typeList = new HashSet<Type>();


        /// <summary>
        /// add type to list
        /// </summary>
        /// <param name="type"></param>
        [Discardable]
        public DistinctTypeList Add(Type type)
        {
            _typeList.Add(type);
            return this;
        }

        /// <summary>
        /// add range of types
        /// </summary>
        /// <param name="types"></param>
        public DistinctTypeList AddRange(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Add(type);
            }
            return this;
        }

        /// <summary>
        /// add all types of DistinctTypeList
        /// </summary>
        /// <param name="typeList"></param>
        public DistinctTypeList AddRange(DistinctTypeList typeList)
        {
            foreach (var type in typeList.GetAllTypes())
            {
                Add(type);
            }
            return typeList;
        }

        /// <summary>
        /// checks if type is already in list
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ContainsType(Type type) => _typeList.Contains(type);

        /// <summary>
        /// is list currently empty
        /// </summary>
        public bool IsEmpty => _typeList.Count == 0;

        /// <summary>
        /// returns all tracked types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetAllTypes() => _typeList;

    }
}
