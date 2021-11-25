using CKO.Payments.Bank.Models;

namespace CKO.Payments.Bank.Client.Interface
{
    public interface IBankClient
    {
        BankResponseModel ProcessPayment(PaymentProcessingModel model);
        BankResponseModel SettlePayment(PaymentSettlementModel model);

    }
}
