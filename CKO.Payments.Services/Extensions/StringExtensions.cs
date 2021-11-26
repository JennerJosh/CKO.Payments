
namespace CKO.Payments.BL.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// This extension will take a string and replace contents with a mask
        /// </summary>
        /// <param name="value">String to be masked</param>
        /// <param name="maskChar">Mask character, this is defaulted to *</param>
        /// <param name="unMaskedLength">Length of unmasked potion of string, this is defaulted to 3</param>
        /// <returns>A masked string representation of the original string</returns>
        public static string Mask(this string value, char maskChar = '*', int unMaskedLength = 3)
        {
            if(string.IsNullOrEmpty(value))
                return value;

            var len = value.Length;
            return new string(maskChar, len - unMaskedLength) + value.Substring(len - unMaskedLength);
        }
    }
}
