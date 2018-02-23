using Domain;
using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Commands
{
    public class CreateAccount : ICommand
    {
        public Guid CustomerId { get; set; }
        public decimal StartingBalance { get; set; }
    }
}
