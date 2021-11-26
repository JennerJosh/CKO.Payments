
namespace CKO.Payments.Data.DTO
{
    public partial class Transaction
    {
        public void SetStatus(int status, string statusMessage)
        {
            Status = status;
            StatusMessage = statusMessage;
        }
    }
}
