using Customers.Policies;
using System;
using System.Collections.Generic;
using System.Text;
using Customers.Exceptions;

namespace Customers
{
    public class EmailAddress
    {
        public string Value { get; }

        public EmailAddress(string email)
        {
            var emailPolicy = new ValidEmailAddressPolicy();

            if (!emailPolicy.IsSatisfiedBy(email))
            {
                throw new InvalidCustomerEmailException(email);
            }

            Value = email;
        }

        public static implicit operator EmailAddress(string value)
        {
            return new EmailAddress(value);
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress.Value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
