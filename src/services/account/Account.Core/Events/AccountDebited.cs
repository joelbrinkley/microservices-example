using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class AccountDebited : DomainEvent
    {
        public const string EVENT_NAMESPACE = "Account.MoneyWithdrawn";

        public AccountDebited(Guid bankAccountId, decimal DebitAmount) 
            : base(bankAccountId.ToString(), typeof(BankAccount).ToString(), EVENT_NAMESPACE)
        {
            this.DebitAmount = DebitAmount;
        }

        public decimal DebitAmount { get; }
    }
}
