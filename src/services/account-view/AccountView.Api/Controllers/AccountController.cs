using AccountView.Api.Queries;
using AccountView.Api.ViewModels;
using AccountView.Data;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountView.Api.Accounts
{
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IQueryHandler<FindAccountQuery, AccountViewModel> findAccountQueryHandler;
        private readonly IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountViewModel>> getAllAccountsQueryHandler;

        public AccountController(IQueryHandler<FindAccountQuery, AccountViewModel> findAccountQueryHandler,
                                 IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountViewModel>> getAllAccountsQueryHandler)   
        {
            this.findAccountQueryHandler = findAccountQueryHandler;
            this.getAllAccountsQueryHandler = getAllAccountsQueryHandler;
        }

        [Route(""), HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await this.getAllAccountsQueryHandler.Execute(new GetAllAccountsQuery());
            return Ok(accounts);
        }

        [Route("{id}"), HttpGet]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var account = await this.findAccountQueryHandler.Execute(new FindAccountQuery() { AccountId = accountId });
            return Ok(account);
        }
    }
}
