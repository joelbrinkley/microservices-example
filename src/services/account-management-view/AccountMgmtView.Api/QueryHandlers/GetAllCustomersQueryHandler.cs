using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Queries;
using AccountMgmtView.Api.Queries;
using AccountMgmtView.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using AccountMgmtView.Data;

namespace AccountMgmtView.Api.QueryHandlers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly AccountMgmtViewContext context;

        public GetAllCustomersQueryHandler(AccountMgmtViewContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CustomerViewModel>> Execute(GetAllCustomersQuery query)
        {
            var customers = await this.context.Customers.ToListAsync();
            var viewModels = customers.Select(x => CustomerViewModel.CreateFor(x));
            return viewModels;
        }
    }
}
