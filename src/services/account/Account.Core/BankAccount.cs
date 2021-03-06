﻿using Account.Events;
using Domain.Aggregates;
using System;

namespace Account
{
    public class BankAccount : Aggregate<BankAccount>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Balance { get; private set; }

        public BankAccount()
        {
            this.Balance = 0;
        }

        public void Withdraw(decimal amount)
        {
            if (this.Balance - amount >= 0)
            {
                this.Apply(new AccountDebited(this.Id, amount));
            }
            else
            {
                throw new InsufficentFundsException();
            }
        }

        public void Deposit(decimal amount)
        {
            this.Apply(new AccountCredited(this.Id, amount));
        }

        public static BankAccount CreateAccount(Guid customerId, decimal amount)
        {

            var account = new BankAccount();

            if (amount >= 0)
            {
                account.Apply(new AccountCreated(Guid.NewGuid(), customerId, amount));
            }
            return account;
        }
        
        private void When(AccountDebited @event)
        {
            this.Balance = this.Balance - @event.DebitAmount;
        }

        private void When(AccountCredited @event)
        {
            this.Balance = this.Balance + @event.CreditAmount;
        }

        private void When(AccountCreated @event)
        {
            this.Id = new Guid(@event.AggregateId);
            this.Balance = @event.StartingBalance;
            this.CustomerId = @event.CustomerId;
        }
    }
}
