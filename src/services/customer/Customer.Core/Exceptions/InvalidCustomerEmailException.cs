using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Exceptions
{
    public class InvalidCustomerEmailException : Exception
    {
        public InvalidCustomerEmailException(string email) : base($"The email, {email}, is not in a valid format.")
        {

        }
    }
}
