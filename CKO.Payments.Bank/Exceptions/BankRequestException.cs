using System.Runtime.Serialization;

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
