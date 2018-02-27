using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Queries;
using AccountView.Api.Queries;
using AccountView.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using AccountView.Data;

namespace AccountView.Api.QueryHandlers
{
    public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountViewModel>>
    {
        private readonly AccountViewContext context;

        public GetAllAccountsQueryHandler(AccountViewContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AccountViewModel>> Execute(GetAllAccountsQuery query)
        {
            var accounts = await this.context.Accounts
                                             .Include(x => x.Customer)
                                             .Include(x=>x.Transactions)
                                             .ToListAsync();
            var viewModels = accounts.Select(x => AccountViewModel.CreateFor(x));
            return viewModels;
        }
    }
}
