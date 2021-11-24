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

        public Merchant RegisterMerchant(Merchant merchant)
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

            _unitOfWork.merchantRepository.AddMerchant(DtoObject);

            // Set the Id of the BL object to be the DTO Id
            merchant.Id = DtoObject.Id;

            return merchant;
        }

        public Merchant GetMerchantFromEmail(string email)
        {
            var merchant = _unitOfWork.merchantRepository.GetMerchantByEmail(email);

            if (merchant == null)
                throw new NotRegisteredException($"Merchant with the email {email} could not be found, please register");

            return MerchantMapper.GetBLMerchant(merchant);
        }

        public bool HasMerchantPreviouslyRegistered(string email) =>
            _unitOfWork.merchantRepository.IsMerchantRegistered(email);
    }
}
