using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Account.Commands;
using Account.Queries;
using Domain.Commands;
using Domain.Queries;

namespace Account
{

    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IQueryHandler<FindAccountQuery, BankAccount> findAccountQueryHandler;
        private readonly ICommandHandler<CreateAccount> createAccountHandler;
        private readonly ICommandHandler<WithdrawFromBankAccount> withdrawHandler;
        private readonly ICommandHandler<DepositMoneyIntoAccount> depositHandler;

        public AccountController(IQueryHandler<FindAccountQuery, BankAccount> findAccountQueryHandler,
                                 ICommandHandler<CreateAccount> createAccountHandler,
                                 ICommandHandler<WithdrawFromBankAccount> withdrawHandler,
                                 ICommandHandler<DepositMoneyIntoAccount> depositHandler)
                            {
                                this.findAccountQueryHandler = findAccountQueryHandler;
                                this.createAccountHandler = createAccountHandler;
                                this.withdrawHandler = withdrawHandler;
                                this.depositHandler = depositHandler;
                            }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetBankAccount(Guid id)
        {
            var query = new FindAccountQuery() { Id = id };

            var bankAccount = await this.findAccountQueryHandler.Execute(query);

            return Ok(bankAccount);
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