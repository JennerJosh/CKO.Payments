using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
