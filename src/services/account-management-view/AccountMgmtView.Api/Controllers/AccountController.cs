using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmtView.Api.Accounts
{
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        [Route(""), HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        [Route("{id}"), HttpGet]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
