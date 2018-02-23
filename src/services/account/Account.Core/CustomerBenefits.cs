using System;
using System.Collections.Generic;
using System.Text;

namespace Account
{
    public class CustomerBenefits
    {
        public static decimal CalculateOpeningAccountBalance(Customer customer, decimal initialFunds)
        {
            decimal funds = initialFunds;

            if (customer.IsPreferredCustomer)
            {
                funds = funds + 500;
            }

            return funds;
        }
    }
}
