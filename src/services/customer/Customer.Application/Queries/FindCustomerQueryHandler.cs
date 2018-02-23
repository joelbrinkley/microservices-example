using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Queries
{
    public class FindCustomerQueryHandler : IQueryHandler<FindCustomerQuery, Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public FindCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Customer> Execute(FindCustomerQuery query)
        {
            var customer = await customerRepository.Find(query.Id);

            return customer;
        }
    }
}
