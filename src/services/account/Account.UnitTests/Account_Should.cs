using Account;
using Account.Events;
using Domain.DomainEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Account.UnitTests
{
    [TestClass]
    public class Account_Should
    {
        public Guid customerId = Guid.NewGuid();

        [TestMethod]
        public void DeclareInsufficentFunds()
        {
            var account = NewAccountWithBalance(50);

            Assert.ThrowsException<InsufficentFundsException>(() => account.Withdraw(75));

        }

        [TestMethod]
        public void WithdrawFunds()
        {
            var account = NewAccountWithBalance(100);

            account.Withdraw(50);

            Assert.AreEqual(50, account.Balance);

        }

        [TestMethod]
        public void CreateAccount()
        {
            var account = BankAccount.CreateAccount(customerId, 500);

            Assert.AreEqual(this.customerId, account.CustomerId);
            Assert.AreEqual(500, account.Balance);
            Assert.IsNotNull(account.Id);
        }

        [TestMethod]
        public void DepositFunds()
        {
            var account = NewAccountWithBalance(100);

            account.Deposit(50);

            Assert.AreEqual(150, account.Balance);
        }

        private BankAccount NewAccountWithBalance(decimal amount)
        {
            var events = new List<DomainEvent<BankAccount>>()
            {
                new AccountCreated(Guid.NewGuid(), customerId, amount)
            };

            var bankAccount = new BankAccount();

            bankAccount.LoadFromHistory(events);

            return bankAccount;
        }

        [TestMethod]
        public void LoadFromHistory()
        {
            var bankAccountId = Guid.NewGuid();

            var events = new List<DomainEvent<BankAccount>>()
            {
                new AccountCreated(bankAccountId, customerId, 100),
                new AccountDebited(bankAccountId, 50),
                new AccountCredited(bankAccountId, 10)
            };

            var bankAccount = new BankAccount();
            bankAccount.LoadFromHistory(events);

            Assert.AreEqual(bankAccountId, bankAccount.Id);
            Assert.AreEqual(60, bankAccount.Balance);
            Assert.AreEqual(customerId, bankAccount.CustomerId);
        }
    }
}
