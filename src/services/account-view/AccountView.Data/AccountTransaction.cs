using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Data
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public AccountTransactionType AccountTransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Occurred { get; set; }
    }

    public enum AccountTransactionType
    {
        Withdraw, 
        Deposit
    }
}
