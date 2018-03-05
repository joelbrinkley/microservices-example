using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class AccountCredited : DomainEvent<BankAccount>
    {
        public const string EVENT_NAMESPACE = "Account.AccountCredited";


        public AccountCredited(Guid bankAccountId, decimal creditAmount)
            : base(bankAccountId.ToString(), EVENT_NAMESPACE)
        {
            CreditAmount = creditAmount;
        }

        public decimal CreditAmount { get; }
    }
}
