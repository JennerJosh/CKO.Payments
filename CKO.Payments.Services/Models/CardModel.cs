using System.Text.RegularExpressions;

namespace CKO.Payments.BL.Models
{
    public class CardModel
    {
        private const string VISA_VALIDATION_REGEX = "^4[0-9]{12}(?:[0-9]{3})?$";
        private const string MASTERCARD_VALIDATION_REGEX = "^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$";
        private const string AMEX_VALIDATION_REGEX = "^3[47][0-9]{13}$";
        private const int MIN_EXPIRY_MONTH = 1;
        private const int MAX_EXPIRY_MONTH = 12;
        private const int MIN_EXPIRY_YEAR = 2000;
        private const int MAX_EXPIRY_YEAR = 2030;
        private const int MIN_CVV_VALUE = 100;
        private const int MAX_CVV_VALUE = 999;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Cvv { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name)
                && (IsCardNumberValid())
                && (ExpiryMonth >= MIN_EXPIRY_MONTH && ExpiryMonth <= MAX_EXPIRY_MONTH)
                && (ExpiryYear >= MIN_EXPIRY_YEAR && ExpiryYear <= MAX_EXPIRY_YEAR)
                && (Cvv >= MIN_CVV_VALUE && Cvv <= MAX_CVV_VALUE);
        }

        private bool IsCardNumberValid()
        {
            return IsValidVisaCardNumber()
                || IsValidMastercardCardNumber()
                || IsValidAmexCardNumber();
        }

        private bool IsValidVisaCardNumber()
        {
            return Regex.IsMatch(Number, VISA_VALIDATION_REGEX);
        }

        private bool IsValidMastercardCardNumber()
        {
            return Regex.IsMatch(Number, MASTERCARD_VALIDATION_REGEX);
        }

        private bool IsValidAmexCardNumber()
        {
            return Regex.IsMatch(Number, AMEX_VALIDATION_REGEX);
        }
    }
}
