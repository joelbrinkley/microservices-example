using AccountView.Api.Queries;
using AccountView.Api.ViewModels;
using AccountView.Data;
using Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountView.Api.QueryHandlers
{
    public class FindAccountQueryHandler : IQueryHandler<FindAccountQuery, AccountViewModel>
    {
        private readonly AccountViewContext context;

        public FindAccountQueryHandler(AccountViewContext context)
        {
            this.context = context;
        }

        public async Task<AccountViewModel> Execute(FindAccountQuery query)
        {
            var account = await this.context.Accounts.FirstOrDefaultAsync(x => x.AccountObjectId == query.AccountId);
            return AccountViewModel.CreateFor(account);
        }
    }
}
