using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class MoneyWithdrawn : DomainEvent<BankAccount>
    {
        public const string EVENT_NAMESPACE = "Account.MoneyWithdrawn";

        public MoneyWithdrawn(Guid bankAccountId, decimal withdrawAmount) 
            : base(bankAccountId.ToString(), EVENT_NAMESPACE)
        {
            WithdrawAmount = withdrawAmount;
        }

        public decimal WithdrawAmount { get; }
    }
}
