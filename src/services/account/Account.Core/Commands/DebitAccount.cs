using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Commands
{
    public class DebitAccount : ICommand
    {
        public Guid BankAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
