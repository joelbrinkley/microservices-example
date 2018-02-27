using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Customers.Commands;
using System.Threading.Tasks;

namespace Customers.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomer>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task Handle(CreateCustomer command)
        {
            var name = new Name(command.FirstName, command.MiddleName ?? "", command.LastName);
            var address = new Address(command.AddressLine1, command.AddressLine2, command.City, command.State, command.ZipCode);
            EmailAddress emailAddress = command.EmailAddress;

            var newCustomer = Customer.Create(name, address, emailAddress, command.IsPreferredCustomer);

            await customerRepository.AddOrUpdate(newCustomer);
        }
    }
}
