using Customers;
using Customers.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Api.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = this.customerRepository.Find(id);
            return Ok(customer);
        }

        [Route(""), HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            throw new NotImplementedException();
        }

    }
}
