using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HierarchyBuilder.Abstractions;
using HierarchyBuilder.Exceptions;

namespace HierarchyBuilder
{
    /// <summary>
    /// <inheritdoc cref="IHierarchyTransformer"/>
    /// </summary>
    public class Transformer : IHierarchyTransformer
    {
        /// <inheritdoc />
        public IEnumerable<T> Transform<T>(IEnumerable<T> collection, Action<TransformationConfiguration<T>> configuration)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            
            var config = new TransformationConfiguration<T>();
            configuration.Invoke(config);

            return Transform(collection, config);
        }

        /// <inheritdoc />
        public IEnumerable<T> Transform<T>(IEnumerable<T> enumerable, TransformationConfiguration<T> configuration)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var collection = enumerable.ToList();
            if (!collection.Any())
                return new List<T>();

            return Build(collection, configuration.IdExpression.Compile(), configuration.ParentIdExpression.Compile(), configuration.ChildrenExpression);
        }
        
        private List<T> Build<T, TKey>(List<T> collection, Func<T, TKey> id, Func<T, TKey> parentId, Expression<Func<T, List<T>>> children)
        {
            var itemLookup = collection.ToDictionary(i => id.Invoke(i) , i => i);
            var result = new List<T>();
        
            foreach (var item in collection)
            {
                if (parentId.Invoke(item) == null)
                {
                    result.Add(item);
                }
                else
                {
                    if (itemLookup.TryGetValue(parentId.Invoke(item), out var parent))
                    {
                        EnsureListIsNotNull(parent, children);
                        var childCollection = children.Compile().Invoke(parent);
                        childCollection.Add(item);
                    }
                }
            }
    
            return result;
        }

        private void EnsureListIsNotNull<T>(T obj, Expression<Func<T, List<T>>> childrenExpression)
        {
            var func = childrenExpression.Compile();
            List<T> children = func(obj);

            if (children == null)
            {
                var listType = typeof(List<>).MakeGenericType(obj.GetType());
                var newList = Activator.CreateInstance(listType);

                if (!(childrenExpression.Body is MemberExpression expression))     
                    throw new TransformationException("Could not find or the property name for children.");
                
                var propertyName = expression.Member.Name;
                
                var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.PropertyType.IsAssignableFrom(listType))
                {
                    propertyInfo.SetValue(obj, newList);
                }
                else
                {
                    throw new TransformationException("Could not find or set the Children property.");
                }
            }
        }
    }
}