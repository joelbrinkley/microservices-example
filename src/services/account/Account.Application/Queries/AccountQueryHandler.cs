using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Queries
{
    public class FindBankAccountQueryHandler : IQueryHandler<FindAccountQuery, BankAccount>
    {
        public BankAccount Execute(FindAccountQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
