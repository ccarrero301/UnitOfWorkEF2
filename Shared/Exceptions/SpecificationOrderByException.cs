namespace Shared.Exceptions
{
    using System;

    public class SpecificationOrderByException : Exception
    {
        public SpecificationOrderByException(string message) : base(message)
        {
        }
    }
}