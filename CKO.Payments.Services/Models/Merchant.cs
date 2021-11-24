using CKO.Payments.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CKO.Payments.BL.Models
{
    public class Merchant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }

        private const int NAME_MAX_LENGTH = 255;
        private const int EMAIL_MAX_LENGTH = 255;
        private const string EMAIL_REGEX = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public Merchant(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Merchant(Guid id, string name, string email, string secret) : this(name, email)
        {
            Id = id;
            Secret = secret;
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
