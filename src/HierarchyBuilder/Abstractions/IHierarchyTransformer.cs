using System;
using System.Collections.Generic;

namespace HierarchyBuilder.Abstractions
{
    /// <summary>
    /// Converts flat lists into the hierarchy models and transform data as required
    /// </summary>
    public interface IHierarchyTransformer
    {
        /// <summary>
        /// Transform given list to a model with a hierarchy
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="configuration"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Transform<T>(IEnumerable<T> collection, Action<TransformationConfiguration<T>> configuration);
        
        /// <summary>
        /// Transform given list to a model with a hierarchy
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="configuration"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Transform<T>(IEnumerable<T> collection, TransformationConfiguration<T> configuration);
    }
}