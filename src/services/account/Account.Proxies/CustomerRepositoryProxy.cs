using Account.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Account.Customers
{
    public class CustomerRepositoryProxy : ICustomerRepository
    {
        private readonly string baseUrl;
        private readonly ILog log;

        public CustomerRepositoryProxy(string baseUrl, ILog log)
        {
            this.baseUrl = baseUrl;
            this.log = log;
        }

        public async Task<Customer> Find(Guid customerId)
        {
            using (var httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
            {
                try
                {
                    var customerResponse = await httpClient.GetAsync($"api/customers/{customerId}");
                    return customerResponse?.TransformToCustomer();
                }
                catch (Exception e)
                {
                    this.log.Error("Error recieved from Customer Service", e);
                }
                return null;
            }
        }
    }
}
