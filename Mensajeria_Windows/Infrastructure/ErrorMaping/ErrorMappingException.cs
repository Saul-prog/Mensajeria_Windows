using System;

namespace Mensajeria_Windows.Infrastructure.ErrorMapping
{
    [Serializable]
    public class ErrorMappingException : Exception
    {
        public ErrorMappingException()
        {
        }

        public ErrorMappingException(string message)
            : base(message)
        {
        }

        public ErrorMappingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ErrorMappingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
