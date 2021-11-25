using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Models;
using CKO.Payments.BL.Exceptions.Transactions;
using CKO.Payments.BL.Mappers;
using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.DAL.Interfaces;
using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.BL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankClient _bankClient;

        public TransactionsService(IUnitOfWork unitOfWork, IBankClient bankClient)
        {
            _unitOfWork = unitOfWork;
            _bankClient = bankClient;
        }

        /// <summary>
        /// Add stub transaction record to the database
        /// </summary>
        /// <param name="transaction">TransactionModel that will be converted and saved to DB</param>
        /// <returns>TransactionModel with ID updated to reflect new Transaction ID</returns>
        /// <exception cref="InvalidTransactionException">Invalid value in either Account or Currency</exception>
        public TransactionModel AddTransactionStub(TransactionModel transaction)
        {
            // Validate transation model is correct
            if (!transaction.IsStubValid())
                throw new InvalidTransactionException("Amount or Currency is invalid, please check and try again");

            // Map to DTO Object
            var dtoObject = TransactionMapper.MapToTransaction(transaction);

            // Save transaction into DB
            _unitOfWork.TransactionRepository.AddTransaction(dtoObject);

            // Set Transaction Id to Model Id
            transaction.Id = dtoObject.Id;

            // return model with Db Id
            return transaction;
        }

        /// <summary>
        /// Updates Existing Transaction record, this is the passed across to the bank to processing
        /// Transaction record statuses are updated to reflect outcomes
        /// </summary>
        /// <param name="transaction">TransactionModel to process</param>
        /// <returns>Response containing transaction id, payment id or messagew from bank if it was not successful</returns>
        /// <exception cref="InvalidTransactionException">An ascpect of the TransactionModel is invalid and requires checking</exception>
        public PaymentProcessingResponseModel ProcessTransaction(TransactionModel transaction)
        {
            // Validate model is correct
            if (!transaction.IsValid())
                throw new InvalidTransactionException("Invalid request, please check model and try again.");

            // Update Status of the transaction
            transaction.SetStatus((int)TransactionStatus.Processing, "Transaction Processing");

            // Send transaction to DB to store details
            UpdateTransaction(transaction);

            // Convert model to bank processing model
            var processingModel = new Bank.Models.PaymentProcessingModel();

            // Send request to Bank to process
            var bankResponse = _bankClient.ProcessPayment(processingModel);

            // Process response from Bank
            if (bankResponse.IsSuccess)
            {
                transaction.SetStatus((int)TransactionStatus.Approved, "Transaction is approved, awaiting settlement");
                transaction.SetPaymentId(bankResponse.PaymentId);
            }
            else
            {
                transaction.SetStatus((int)TransactionStatus.Declined, $"Transaction Declined: {bankResponse.Message}");
            }

            // Send status update to DB
            UpdateTransaction(transaction);

            // return response
            return PaymentProcessingMapper.MapToPaymentProcessingResponseModel(bankResponse, transaction);
        }

        /// <summary>
        /// Sends transaction to DAL to update database record
        /// </summary>
        /// <param name="transaction"></param>
        public void UpdateTransaction(TransactionModel transaction)
        {
            // Convert transaction into DTO object
            var dtoObject = TransactionMapper.MapToTransaction(transaction);
            // Send transaction to DB to store details
            _unitOfWork.TransactionRepository.UpdateTransaction(dtoObject);
        }

        public PaymentSettlementResponseModel SettleTransaction(TransactionModel transaction)
        {
            // Get the transaction from the database
            var dtoObject = _unitOfWork.TransactionRepository.GetTransaction(transaction.Id);

            // Validate transaction record exists and the provided payment id belongs to this transaction
            if (dtoObject == null)
                return new PaymentSettlementResponseModel(false, "Transaction could not be found or has already been completed");

            if (dtoObject.BankPaymentId != transaction.BankPaymentId)
                return new PaymentSettlementResponseModel(false, "Payment Id does not match");

            // Create settlement model
            var settlementModel = new PaymentSettlementModel(transaction.BankPaymentId);

            // Send request to bank to settle payment
            var bankResponse = _bankClient.SettlePayment(settlementModel);

            // Process response from the bank
            if (bankResponse.IsSuccess)
            {
                transaction.SetStatus((int)TransactionStatus.Settled, "Transaction has been settled");
            }
            else
            {
                transaction.SetStatus((int)TransactionStatus.Declined, $"Transaction could not be settled: {bankResponse.Message}");
            }

            // Update database record
            UpdateTransaction(transaction);

            // Return result
            return new PaymentSettlementResponseModel(bankResponse.IsSuccess, bankResponse.Message);
        }

        /// <summary>
        /// Retrieve a list of transactions that are linked to the merchant
        /// </summary>
        /// <param name="MerchantId">Id of the merchant</param>
        /// <returns>List of transactions</returns>
        public List<TransactionModel> GetMerchantTransactions(Guid merchantId)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactionsByMerchant(merchantId);

            return transactions
                .Select(x => TransactionMapper.MapToTransactionModel(x))
                .ToList();
        }

    }
}
