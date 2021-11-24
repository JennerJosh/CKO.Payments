using System.Runtime.Serialization;

namespace CKO.Payments.BL.Exceptions.Merchants
{
    public class NotRegisteredException : Exception
    {
        public NotRegisteredException()
        {
        }

        public NotRegisteredException(string? message) : base(message)
        {
        }

        public NotRegisteredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
