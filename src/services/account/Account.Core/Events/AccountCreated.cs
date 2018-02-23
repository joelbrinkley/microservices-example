using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class AccountCreated : DomainEvent<BankAccount>
    {
        public const string EVENT_NAMESPACE = "Account.Created";

        public AccountCreated(Guid bankAccountId, Guid customerId, decimal amount)
            : base(bankAccountId.ToString(), EVENT_NAMESPACE)
        {
            StartingBalance = amount;
            CustomerId = customerId;
        }


        public decimal StartingBalance { get; }
        public Guid CustomerId { get; }
    }
}
