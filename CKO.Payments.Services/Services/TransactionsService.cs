using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Models;
using CKO.Payments.BL.Exceptions.Transactions;
using CKO.Payments.BL.Mappers;
using CKO.Payments.BL.Models.Responses;
using CKO.Payments.BL.Models.Transactions;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.DAL.Interfaces;
using CKO.Payments.Data.DTO;
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
        public async Task<PaymentProcessingResponseModel> ProcessTransactionAsync(TransactionModel transaction)
        {
            // Validate model is correct
            if (!transaction.IsValid())
                throw new InvalidTransactionException("Invalid request, please check model and try again.");

            // Fetch transaction from the database
            // Onl retrieve transaction if it is in the Pending status
            var dtoObject = _unitOfWork.TransactionRepository.GetPendingTransaction(transaction.Id);

            if (dtoObject == null)
                throw new InvalidTransactionException("Transaction could not be found, it has either already been processed or transaction id is incorrect");

            // Update Status of the transaction
            dtoObject.SetStatus((int)TransactionStatus.Processing, "Transaction Processing");

            // Send transaction to DB to store details
            _ = UpdateTransaction(transaction);

            // Convert model to bank processing model
            var processingModel = new PaymentProcessingModel();

            // Send request to Bank to process
            var bankResponse = await _bankClient.ProcessPaymentAsync(processingModel);

            // Process response from Bank
            if (bankResponse.IsSuccess)
            {
                dtoObject.SetStatus((int)TransactionStatus.Approved, "Transaction is approved, awaiting settlement");
                dtoObject.BankPaymentId = bankResponse.PaymentId;
            }
            else
            {
                dtoObject.SetStatus((int)TransactionStatus.Declined, $"Transaction Declined: {bankResponse.Message}");
            }

            // Send status update to DB
            _ = UpdateTransaction(dtoObject);

            // Map DTO object back to TransactionModel
            transaction = TransactionMapper.MapToTransactionModel(dtoObject);

            // return response
            return PaymentProcessingMapper.MapToPaymentProcessingResponseModel(bankResponse, transaction);
        }

        /// <summary>
        /// Sends transaction to DAL to update database record
        /// </summary>
        /// <param name="transaction"></param>
        public Transaction UpdateTransaction(TransactionModel transaction)
        {
            // Convert transaction into DTO object
            var dtoObject = TransactionMapper.MapToTransaction(transaction);
            // Send transaction to DB to store details
            _unitOfWork.TransactionRepository.UpdateTransaction(dtoObject);

            return dtoObject;
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            // Send transaction to DB to store details
            _unitOfWork.TransactionRepository.UpdateTransaction(transaction);

            return transaction;
        }

        public async Task<PaymentSettlementResponseModel> SettleTransactionAsync(TransactionModel transaction)
        {
            // Get the transaction from the database
            // Only approved transactions can be settled
            var dtoObject = _unitOfWork.TransactionRepository.GetApprovedTransaction(transaction.Id);

            // Validate transaction record exists and the provided payment id belongs to this transaction
            if (dtoObject == null)
                return new PaymentSettlementResponseModel(false, "Transaction could not be found or has already been completed");

            if (dtoObject.BankPaymentId != transaction.BankPaymentId)
                return new PaymentSettlementResponseModel(false, "Payment Id does not match");

            // Create settlement model
            var settlementModel = new PaymentSettlementModel(transaction.BankPaymentId);

            // Send request to bank to settle payment
            var bankResponse = await _bankClient.SettlePaymentAsync(settlementModel);

            // Process response from the bank
            if (bankResponse.IsSuccess)
            {
                dtoObject.SetStatus((int)TransactionStatus.Settled, "Transaction has been settled");
            }
            else
            {
                dtoObject.SetStatus((int)TransactionStatus.Declined, $"Transaction could not be settled: {bankResponse.Message}");
            }

            // Update database record
            _ = UpdateTransaction(dtoObject);

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
                .Select(x =>
                {
                    var obj = TransactionMapper.MapToTransactionModel(x);
                    obj.MaskSensitiveData();

                    return obj;
                })
                .ToList();
        }

    }
}
