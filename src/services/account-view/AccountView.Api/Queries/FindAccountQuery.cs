using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountView.Api.Queries
{
    public class FindAccountQuery : IQuery
    {
        public Guid AccountId { get; set; }
    }
}
