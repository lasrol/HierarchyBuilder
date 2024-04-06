using System;

namespace HierarchyBuilder.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class TransformationException : Exception
    {
        public TransformationException(string message)
            : base(message)
        { }
    }
}