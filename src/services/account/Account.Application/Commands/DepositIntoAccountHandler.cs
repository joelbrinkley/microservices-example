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

        public DepositIntoAccountHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(DepositMoneyIntoAccount command)
        {
            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Deposit(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);
        }
    }
}
