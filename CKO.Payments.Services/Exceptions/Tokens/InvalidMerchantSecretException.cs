using System.Runtime.Serialization;

namespace CKO.Payments.BL.Exceptions.Tokens
{
    public class InvalidMerchantSecretException : Exception
    {
        public InvalidMerchantSecretException()
        {
        }

        public InvalidMerchantSecretException(string? message) : base(message)
        {
        }

        public InvalidMerchantSecretException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidMerchantSecretException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
