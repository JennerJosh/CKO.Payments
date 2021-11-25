using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.BL.Exceptions.Transactions;
using CKO.Payments.BL.Mappers;
using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.DAL.Interfaces;
using static CKO.Payments.BL.Enums.Transactions;

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
        /// <returns>TransactionModel</returns>
        /// <exception cref="InvalidTransactionException">An ascpect of the TransactionModel is invalid and requires checking</exception>
        public TransactionModel ProcessTransaction(TransactionModel transaction)
        {
            // Validate model is correct
            if (!transaction.IsValid())
                throw new InvalidTransactionException("Invalid request, please check model and try again.");

            // Update Status of the transaction
            transaction.Status = (int)TransactionStatus.Processing;
            transaction.StatusMessage = "Transaction Processing";

            // Send transaction to DB to store details
            UpdateTransaction(transaction);

            // Convert model to bank processing model
            var processingModel = new Bank.Models.PaymentProcessingModel();

            // Send request to Bank to process
            var bankResponse = _bankClient.ProcessPayment(processingModel);
            // TODO: Connect to Bank API and await response

            // Process response from Bank
            if (bankResponse.IsSuccess)
            {
                transaction.Status = (int)TransactionStatus.Approved;
                transaction.StatusMessage = "Transaction is approved, awaiting settlement";
            }
            else
            {
                transaction.Status = (int)TransactionStatus.Declined;
                transaction.StatusMessage = $"Transaction Declined: {bankResponse.Message}";
            }

            // Send status update to DB
            UpdateTransaction(transaction);

            // return response
            return transaction;

        }

        private void UpdateTransaction(TransactionModel transaction)
        {
            // Convert transaction into DTO object
            var dtoObject = TransactionMapper.MapToTransaction(transaction);
            // Send transaction to DB to store details
            _unitOfWork.TransactionRepository.UpdateTransaction(dtoObject);
        }

    }
}
