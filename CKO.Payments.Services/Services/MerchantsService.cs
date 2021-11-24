using CKO.Payments.BL.Exceptions.Merchants;
using CKO.Payments.BL.Mappers;
using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.DAL.Interfaces;

namespace CKO.Payments.BL.Services
{
    public class MerchantsService : IMerchantsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MerchantsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Register new Merchant
        /// This will validate the model then save it into the database
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns>The merchant object with the DB Id returned with it</returns>
        /// <exception cref="InvalidMerchantException">This occurs when either the Name or Email of the merchant is invalid</exception>
        /// <exception cref="AlreadyRegisteredException">This occurs when the email of the Merchant is already in the database</exception>
        public MerchantModel RegisterMerchant(MerchantModel merchant)
        {
            // Check to see if Merchant is valid before saving
            if (!merchant.IsValid())
                throw new InvalidMerchantException("Name or Email is invalid, please check and try again");

            // Check to see if Merchant is already registed
            if (HasMerchantPreviouslyRegistered(merchant.Email))
                throw new AlreadyRegisteredException($"There is already a merchant account with this email: {merchant.Email}");

            // If secret has not been set, generate new secret
            if (string.IsNullOrEmpty(merchant.Secret))
                merchant.GenerateSecret();

            // Map BL merchant to the DTO object
            var DtoObject = MerchantMapper.GetDTOMerchant(merchant);

            // Add merchant to DB
            _unitOfWork.MerchantRepository.AddMerchant(DtoObject);

            // Set the Id of the BL object to be the DTO Id
            merchant.Id = DtoObject.Id;

            return merchant;
        }


        /// <summary>
        /// Find a Merchant record by their registered email
        /// </summary>
        /// <param name="email">Email of the Merchant</param>
        /// <returns>The found Merchant record</returns>
        /// <exception cref="NotRegisteredException">This is thrown when the Merchant cannot be found</exception>
        public MerchantModel GetMerchantFromEmail(string email)
        {
            var merchant = _unitOfWork.MerchantRepository.GetMerchantByEmail(email);

            if (merchant == null)
                throw new NotRegisteredException($"Merchant with the email '{email}' could not be found, please register and try again");

            return MerchantMapper.GetBLMerchant(merchant);
        }

        /// <summary>
        /// Check to see if the Merchant's email is already in the database
        /// </summary>
        /// <param name="email">Email of the Merchant</param>
        /// <returns>A true or false value representing whether the email has been found</returns>
        public bool HasMerchantPreviouslyRegistered(string email) =>
            _unitOfWork.MerchantRepository.IsMerchantRegistered(email);
    }
}
