using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> Find(Guid customerId);
    }
}
