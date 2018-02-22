using Domain.Policies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Policies
{
    public class ValidEmailAddressPolicy : IPolicy<string>
    {
        public bool IsSatisfiedBy(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
