using CKO.Payments.BL.Extensions;
using CKO.Payments.BL.Helpers;
using System.Text.RegularExpressions;

namespace CKO.Payments.BL.Models.Merchants
{
    public class MerchantModel
    {
        private const int NAME_MAX_LENGTH = 255;
        private const int EMAIL_MAX_LENGTH = 255;
        private const string EMAIL_REGEX = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }

        public MerchantModel(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public MerchantModel(string name, string email, string secret) : this(name, email)
        {
            Secret = secret;
        }

        public MerchantModel(Guid id, string name, string email, string secret) : this(name, email, secret)
        {
            Id = id;
        }

        public bool IsValid()
        {
            if (!IsNameValid() || !IsEmailValid())
                return false;

            return true;
        }

        public void GenerateSecret()
        {
            if (string.IsNullOrEmpty(Email))
                return;

            Secret = SecurityHelper.GenerateHash(Email, salt: null);
        }

        public bool IsSecretValid(string secret)
        {
            return string.Equals(Secret, secret);
        }

        public void MaskSecret()
        {
            Secret = Secret.Mask();
        }

        private bool IsNameValid()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length > NAME_MAX_LENGTH)
                return false;

            return true;
        }

        private bool IsEmailValid()
        {
            if (string.IsNullOrEmpty(Email) || !DoesEmailMatchRegex() || Email.Length > EMAIL_MAX_LENGTH)
                return false;

            return true;
        }

        private bool DoesEmailMatchRegex()
        {
            return Regex.IsMatch(Email, EMAIL_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }


}
