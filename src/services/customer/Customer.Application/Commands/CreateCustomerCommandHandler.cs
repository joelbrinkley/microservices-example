using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Customers.Commands;
using Domain.Commands;
using Customers.Logging;

namespace Customers.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomer>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILog log;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ILog log)
        {
            this.log = log;
            this.customerRepository = customerRepository;
        }

        public async Task Handle(CreateCustomer command)
        {
            log.Information($"Creating new customer: {command.FirstName} {command.LastName}");

            var name = new Name(command.FirstName, command.MiddleName ?? "", command.LastName);
            var address = new Address(command.AddressLine1, command.AddressLine2, command.City, command.State, command.ZipCode);
            EmailAddress emailAddress = command.EmailAddress;

            var newCustomer = Customer.Create(name, address, emailAddress, command.IsPreferredCustomer);

            await customerRepository.AddOrUpdate(newCustomer);

            log.Information($"Created new customer: {newCustomer.Name.FullName} with id {newCustomer.Id}");
        }
    }
}