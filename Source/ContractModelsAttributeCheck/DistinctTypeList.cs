
using System;
using System.Collections.Generic;

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
        public void Add(Type type)
        {
            _typeList.Add(type);
        }

        /// <summary>
        /// add range of types
        /// </summary>
        /// <param name="types"></param>
        public void AddRange(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Add(type);
            }
        }

        /// <summary>
        /// add all types of DistinctTypeList
        /// </summary>
        /// <param name="typeList"></param>
        public void AddRange(DistinctTypeList typeList)
        {
            foreach (var type in typeList.GetAllTypes())
            {
                Add(type);
            }
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
