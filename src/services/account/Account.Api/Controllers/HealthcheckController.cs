using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Controllers
{
    [Route("api/healthcheck")]
    public class HealthcheckController : Controller
    {
        public HealthcheckController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok(new { status = "success", message = "Account Service up and running..." });
        }
    }
}
