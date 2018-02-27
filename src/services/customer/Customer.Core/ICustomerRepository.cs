using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> Find(string id);
        Task AddOrUpdate(Customer customer);
        
    }
}
