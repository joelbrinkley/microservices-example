using Domain.DomainEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Events
{
    public class AccountCreated : DomainEvent
    {
        public const string EVENT_NAMESPACE = "Account.Created";

        public AccountCreated(Guid bankAccountId, Guid customerId, decimal amount)
            : base(bankAccountId.ToString(), typeof(BankAccount).ToString(), EVENT_NAMESPACE)
        {
            StartingBalance = amount;
            CustomerId = customerId;

            this.EventData = JsonConvert.SerializeObject(new { CustomerId, StartingBalance });
        }



        public decimal StartingBalance { get; }
        public Guid CustomerId { get; }
    }
}
