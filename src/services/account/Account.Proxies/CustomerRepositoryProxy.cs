using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Account.Customers
{
    public class CustomerRepositoryProxy : ICustomerRepository
    {
        private readonly string baseUrl;

        public CustomerRepositoryProxy(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<Customer> Find(Guid customerId)
        {
            using (var httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
            {
                var customerResponse = await httpClient.GetAsync($"api/customers/{customerId}");
                return customerResponse?.TransformToCustomer();
            }
        }
    }
}
