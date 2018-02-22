using Account.Logging;
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
        private readonly ILog log;

        public CreateAccountHandler(IBankAccountRepository bankAccountRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.log = log;
        }

        public async Task Handle(CreateAccount command)
        {
            this.log.Information($"Handling create command for customer {command.CustomerId}");

            var newAccount = BankAccount.CreateAccount(command.CustomerId, command.StartingBalance);

            await bankAccountRepository.AddOrUpdate(newAccount);

            this.log.Information($"Account {newAccount.Id} created for {command.CustomerId}");
        }
    }
}
