using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Account.Commands;
using Domain.Commands;
using Domain.Queries;

namespace Account
{

    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly ICommandHandler<CreateAccount> createAccountHandler;
        private readonly ICommandHandler<CreditAccount> creditHandler;
        private readonly ICommandHandler<DebitAccount> debitHandler;

        public AccountController(ICommandHandler<CreateAccount> createAccountHandler,
                                 ICommandHandler<CreditAccount> withdrawHandler,
                                 ICommandHandler<DebitAccount> depositHandler)
                            {
                                this.createAccountHandler = createAccountHandler;
                                this.creditHandler = withdrawHandler;
                                this.debitHandler = depositHandler;
                            }

       
        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccount createAccountCommand)
        {
            await this.createAccountHandler.Handle(createAccountCommand);
            return NoContent();
        }

        [HttpPost, Route("{bankAccountId}/credit")]
        public async Task<IActionResult> CreditAccount(Guid bankAccountId, [FromBody] DebitAccount debitCommand)
        {
            await this.debitHandler.Handle(debitCommand);
            return NoContent();
        }

        [HttpPost, Route("{bankAccountId}/debit")]
        public async Task<IActionResult> DebitAccount(Guid bankAccountId, [FromBody] CreditAccount creditCommand)
        {
            await this.creditHandler.Handle(creditCommand);
            return NoContent();
        }
    }
}