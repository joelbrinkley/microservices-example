using Account.Logging;
using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Commands
{
    public class DepositIntoAccountHandler : ICommandHandler<DepositMoneyIntoAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly ILog log;

        public DepositIntoAccountHandler(IBankAccountRepository bankAccountRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.log = log;
        }

        public async Task Handle(DepositMoneyIntoAccount command)
        {
            this.log.Information($"Handling deposit money command for account {command.BankAccountId}");

            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Deposit(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);

            this.log.Information($"Deposited amount ${command.Amount} into bank account {command.BankAccountId}");
        }
    }
}
