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
using Autofac;
using Customers;
using Customers.Autofac;

namespace Customer.Api
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
            services.AddMvc();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var mongoConnectionString = this.Configuration.GetSection("MongoConnection")["ConnectionString"];
            builder.RegisterModule(new RepositoryModule(mongoConnectionString));
            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new ApplicationModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Debug")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
