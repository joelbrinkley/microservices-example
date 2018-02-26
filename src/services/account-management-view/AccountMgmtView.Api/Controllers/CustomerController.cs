using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmtView.Api.Customers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        public CustomerController()
        {

        }

        public async Task<IActionResult> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        [Route("api/customers/{id}")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
