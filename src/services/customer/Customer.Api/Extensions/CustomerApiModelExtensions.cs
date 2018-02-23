using Customer.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Extensions
{
    public static class CustomerApiModelExtensions
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            return new CustomerViewModel(customer.Name.FirstAndLast, customer.IsPreferredCustomer);
        }
    }
}
