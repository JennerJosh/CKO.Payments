using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Exceptions.Merchants
{
    internal class InvalidMerchantException : Exception
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
