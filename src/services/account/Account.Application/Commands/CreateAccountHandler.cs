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

        public CreateAccountHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(CreateAccount command)
        {
            var newAccount = BankAccount.CreateAccount(command.CustomerId, command.StartingBalance);

            await bankAccountRepository.AddOrUpdate(newAccount);
        }
    }
}
