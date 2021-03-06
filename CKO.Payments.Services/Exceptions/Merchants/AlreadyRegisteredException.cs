using System.Runtime.Serialization;

namespace CKO.Payments.BL.Exceptions.Merchants
{
    internal class AlreadyRegisteredException : Exception
    {
        public AlreadyRegisteredException()
        {
        }

        public AlreadyRegisteredException(string? message) : base(message)
        {
        }

        public AlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
