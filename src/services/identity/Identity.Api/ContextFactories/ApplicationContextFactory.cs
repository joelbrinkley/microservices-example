using Identity.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.ContextFactories
{
    public class ApplicationContextFactory : DesignTimeDbContextFactory<ApplicationDbContext>
    {
    }
}
