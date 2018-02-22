using Account.Logging;
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
        private readonly ILog log;

        public WithdrawFromAccountHandler(IBankAccountRepository bankAccountRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.log = log;
        }

        public async Task Handle(WithdrawFromBankAccount command)
        {
            this.log.Information($"Handling withdraw command for account {command.BankAccountId}");

            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Withdraw(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);

            this.log.Information($"Withdrawed amount ${command.Amount} from bank account {command.BankAccountId}");
        }
    }
}
