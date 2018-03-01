using Account.Customers;
using Account.Exceptions;
using Logging;
using Domain;
using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Commands
{
    public class CreateAccountHandler : ICommandHandler<CreateAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ILog log;

        public CreateAccountHandler(IBankAccountRepository bankAccountRepository, ICustomerRepository customerRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.customerRepository = customerRepository;
            this.log = log;
        }

        public async Task Handle(CreateAccount command)
        {
            this.log.Information($"Handling create command for customer {command.CustomerId}");

            var customer = await customerRepository.Find(command.CustomerId);

            if (customer == null) throw new CustomerNotFoundException(command.CustomerId);

            var startingBalance = CustomerBenefits.CalculateOpeningAccountBalance(customer, command.StartingBalance);

            var newAccount = BankAccount.CreateAccount(command.CustomerId, startingBalance);

            await bankAccountRepository.AddOrUpdate(newAccount);

            this.log.Information($"Account {newAccount.Id} created for {command.CustomerId}");
        }
    }
}
