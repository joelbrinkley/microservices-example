using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> Find(Guid id);
        Task AddOrUpdate(BankAccount bankAccount);
    }
}
