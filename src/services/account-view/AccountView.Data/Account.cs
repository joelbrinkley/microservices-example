using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountView.Data
{
    public class Account 
    {
        public int Id { get; set; }
        public Guid AccountObjectId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Balance { get; set; }
        public DateTime AccountCreatedOn { get; set; }

        public IEnumerable<AccountTransaction> Transactions { get; set; }
    }
}
