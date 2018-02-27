using Account.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Controllers
{
    [Route("api/accounts/healthcheck")]
    public class HealthcheckController : Controller
    {
        private readonly ILog log;

        public HealthcheckController(ILog log)
        {
            this.log = log;
        }

        [HttpPost]
        public async Task<IActionResult> HealthCheck()
        {
            log.Information("Account Service up and running...");
            return Ok(new { status = "success", message = "Account Service up and running..." });
        }
    }
}
