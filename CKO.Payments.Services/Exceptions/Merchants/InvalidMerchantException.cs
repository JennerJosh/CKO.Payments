using System.Runtime.Serialization;

namespace CKO.Payments.BL.Exceptions.Merchants
{
    public class InvalidMerchantException : Exception
    {
        public InvalidMerchantException()
        {
        }

        public InvalidMerchantException(string? message) : base(message)
        {
        }

        public InvalidMerchantException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidMerchantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
