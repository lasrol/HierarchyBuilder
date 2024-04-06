using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HierarchyBuilder
{
    /// <summary>
    /// Add configuration to define how the transformation should be applied
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TransformationConfiguration<T>
    {
        internal Expression<Func<T, object>> IdExpression;
        internal Expression<Func<T, object>> ParentIdExpression;
        internal Expression<Func<T, List<T>>> ChildrenExpression;

        /// <summary>
        /// Set field or property to use as identifier for the objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransformationConfiguration<T> UseId(Expression<Func<T, object>> id)
        {
            IdExpression = id;
            return this;
        }
        
        /// <summary>
        /// Set field or property to use as parent identifier for the objects
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public TransformationConfiguration<T> UseParentId(Expression<Func<T, object>> parentId)
        {
            ParentIdExpression = parentId;
            return this;
        }
        
        /// <summary>
        /// Set field or property to use as parent identifier for the objects
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        public TransformationConfiguration<T> UseChildren(Expression<Func<T, List<T>>> children)
        {
            ChildrenExpression = children;
            return this;
        }
    }
}