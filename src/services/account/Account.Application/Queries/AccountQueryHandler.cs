using Domain;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Queries
{
    public class FindBankAccountQueryHandler : IQueryHandler<FindAccountQuery, BankAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public FindBankAccountQueryHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Execute(FindAccountQuery query)
        {
            var account = await this.bankAccountRepository.Find(query.Id);
            return account;
        }
    }
}
