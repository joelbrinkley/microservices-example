using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Queries
{
    public class FindCustomerQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}
