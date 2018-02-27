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
        private readonly ICommandHandler<WithdrawFromBankAccount> withdrawHandler;
        private readonly ICommandHandler<DepositMoneyIntoAccount> depositHandler;

        public AccountController(ICommandHandler<CreateAccount> createAccountHandler,
                                 ICommandHandler<WithdrawFromBankAccount> withdrawHandler,
                                 ICommandHandler<DepositMoneyIntoAccount> depositHandler)
                            {
                                this.createAccountHandler = createAccountHandler;
                                this.withdrawHandler = withdrawHandler;
                                this.depositHandler = depositHandler;
                            }

       
        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccount createAccountCommand)
        {
            await this.createAccountHandler.Handle(createAccountCommand);
            return NoContent();
        }

        [HttpPost, Route("{bankAccountId}/deposit")]
        public async Task<IActionResult> DepositFunds(Guid bankAccountId, [FromBody] DepositMoneyIntoAccount depositCommand)
        {
            await this.depositHandler.Handle(depositCommand);
            return NoContent();
        }

        [HttpPost, Route("{bankAccountId}/withdrawal")]
        public async Task<IActionResult> WithdrawFunds(Guid bankAccountId, [FromBody] WithdrawFromBankAccount withdrawCommand)
        {
            await this.withdrawHandler.Handle(withdrawCommand);
            return NoContent();
        }
    }
}