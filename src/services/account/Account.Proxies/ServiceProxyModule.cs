using Account.Customers;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Autofac
{
    public class ServiceProxyModule : Module
    {
        private readonly string customerBaseUrl;

        public ServiceProxyModule(string customerBaseUrl)
        {
            this.customerBaseUrl = customerBaseUrl;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(customerBaseUrl)) throw new Exception("Unable to find customer service base url");

            builder.RegisterType<CustomerRepositoryProxy>().As<ICustomerRepository>().WithParameter("baseUrl", customerBaseUrl);
        }
    }
}
