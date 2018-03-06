using Microsoft.Extensions.Configuration;
using Identity.Api.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Data
{
    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            //callbacks urls from config:
            var clientUrls = new Dictionary<string, string>
            {
              
            };

            if (!await context.Clients.AnyAsync())
            {
                context.Clients.AddRange(Config.GetClients(clientUrls).Select(client => client.ToEntity()));
            }

            if (!await context.IdentityResources.AnyAsync())
            {
                context.IdentityResources.AddRange(Config.GetResources().Select(resource => resource.ToEntity()));
            }

            if (!await context.ApiResources.AnyAsync())
            {
                context.ApiResources.AddRange(Config.GetApis().Select(api => api.ToEntity()));
            }

            await context.SaveChangesAsync();
        }
    }
}
