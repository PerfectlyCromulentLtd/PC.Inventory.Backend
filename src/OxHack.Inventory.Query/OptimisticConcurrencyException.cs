using System;
using System.Runtime.Serialization;

namespace OxHack.Inventory.Query
{
    [Serializable]
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException()
        {
        }

        public OptimisticConcurrencyException(string message) : base(message)
        {
        }

        public OptimisticConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OptimisticConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}