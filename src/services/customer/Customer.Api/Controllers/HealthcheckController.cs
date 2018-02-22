using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [Route("api/healthcheck")]
    public class HealthcheckController : Controller
    {
        public HealthcheckController()
        {

        }

        [Route(""), HttpGet]
        public async Task<IActionResult> HealthCheck()
        {

            return Ok(new { status = "success", message = "Customer Service up and running..." });
        }
    }
}
