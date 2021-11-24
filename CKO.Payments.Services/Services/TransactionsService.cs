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

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            // Convert transaction into DTO object
            var dtoObject = TransactionMapper.MapToTransaction(transaction);

            // Update Status of the transaction
            dtoObject.Status = (int)TransactionStatus.Processing;
            dtoObject.StatusMessage = "Transaction Processing";

            // Send transaction to DB to store details
            _unitOfWork.TransactionRepository.UpdateTransaction(dtoObject);

            // Send request to Bank to process
            var bankResponse = true;
            // TODO: Connect to Bank API and await response

            // Process response from Bank
            if (bankResponse)
            {
                dtoObject.Status = (int)TransactionStatus.Settled;
                dtoObject.StatusMessage = "Transaction Settled";
            }
            else
            {
                dtoObject.Status = (int)TransactionStatus.Declined;
                dtoObject.StatusMessage = "Transaction Declined";
            }

            // Send status update to DB
            _unitOfWork.TransactionRepository.UpdateTransaction(dtoObject);

            // return response
            return transaction;

        }

    }
}
