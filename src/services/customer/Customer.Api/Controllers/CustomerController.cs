using Customers;
using Customers.Queries;
using Customers.Commands;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Api.Extensions;

namespace Customers.Api.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICommandHandler<CreateCustomer> createCustomerHandler;
        private readonly IQueryHandler<FindCustomerQuery, Customer> findCustomerQueryHandler;

        public CustomerController(ICommandHandler<CreateCustomer> createCustomerHandler,
                                  IQueryHandler<FindCustomerQuery, Customer> findCustomerQueryHandler)
        {
            this.createCustomerHandler = createCustomerHandler;
            this.findCustomerQueryHandler = findCustomerQueryHandler;
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await this.findCustomerQueryHandler.Execute(new FindCustomerQuery() { Id = id });

            return Ok(customer.ToViewModel());
        }

        [Route(""), HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomer createCustomerCommand)
        {
            await this.createCustomerHandler.Handle(createCustomerCommand);
            return NoContent();
        }

    }
}
