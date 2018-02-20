using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class MoneyDeposited : DomainEvent<BankAccount>
    {
        public const string EVENT_NAMESPACE = "Account.MoneyDeposited";


        public MoneyDeposited(Guid bankAccountId, decimal depositAmount)
            : base(bankAccountId.ToString(), EVENT_NAMESPACE)
        {
            DepositAmount = depositAmount;
        }

        public decimal DepositAmount { get; }
    }
}
