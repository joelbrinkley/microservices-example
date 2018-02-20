using Domain;
using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Commands
{
    public class WithdrawFromAccountHandler : ICommandHandler<WithdrawFromBankAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public WithdrawFromAccountHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(WithdrawFromBankAccount command)
        {
            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Withdraw(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);
        }
    }
}
