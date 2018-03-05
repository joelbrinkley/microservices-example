using Domain;
using Domain.Commands;
using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Commands
{
    public class DebitAccountHandler : ICommandHandler<DebitAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly ILog log;

        public DebitAccountHandler(IBankAccountRepository bankAccountRepository, ILog log)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.log = log;
        }

        public async Task Handle(DebitAccount command)
        {
            this.log.Information($"Handling withdraw command for account {command.BankAccountId}");

            var bankAccount = await bankAccountRepository.Find(command.BankAccountId);

            bankAccount.Withdraw(command.Amount);

            await bankAccountRepository.AddOrUpdate(bankAccount);

            this.log.Information($"Withdrawed amount ${command.Amount} from bank account {command.BankAccountId}");
        }
    }
}
