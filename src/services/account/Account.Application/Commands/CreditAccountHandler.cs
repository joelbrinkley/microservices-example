using Logging;
using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Commands
{
    public class CreditAccountHandler : ICommandHandler<CreditAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly ILog log;

        public CreditAccountHandler(IBankAccountRepository bankAccountRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.log = log;
        }

        public async Task Handle(CreditAccount command)
        {
            this.log.Information($"Handling deposit money command for account {command.BankAccountId}");

            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Deposit(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);

            this.log.Information($"Deposited amount ${command.Amount} into bank account {command.BankAccountId}");
        }
    }
}
