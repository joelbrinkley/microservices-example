using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Domain.Queries;
using AccountView.Api.Queries;
using AccountView.Api.ViewModels;
using AccountView.Data;
using AccountView.Api.QueryHandlers;

namespace AccountView.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AccountViewContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AccountMgmtView")));

            services.AddMvc();

            services.AddTransient<IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountViewModel>>, GetAllAccountsQueryHandler>();
            services.AddTransient<IQueryHandler<FindAccountQuery, AccountViewModel>, FindAccountQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AccountViewContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
