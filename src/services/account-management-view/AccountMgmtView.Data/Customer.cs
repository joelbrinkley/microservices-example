using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmtView.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public Guid CustomerObjectId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
