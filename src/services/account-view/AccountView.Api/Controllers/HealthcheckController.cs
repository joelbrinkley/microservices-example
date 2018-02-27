using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmtView.Controllers
{
    [Route("api/accounts/healthcheck")]
    public class HealthcheckController : Controller
    {
        public HealthcheckController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok(new { status = "success", message = "Account View Service up and running..." });
        }
    }
}
