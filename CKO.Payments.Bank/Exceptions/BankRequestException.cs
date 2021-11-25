using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Exceptions
{
    public class BankRequestException : Exception
    {
        public BankRequestException()
        {
        }

        public BankRequestException(string? message) : base(message)
        {
        }

        public BankRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BankRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
