using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Account.Customers
{
    public static class HttpResponseExtension
    {
        public static Customer TransformToCustomer(this HttpResponseMessage response)
        {
            var jobject = new JObject(response.Content);
            var customer = new Customer(
                    jobject["name"].ToString(),
                    Convert.ToBoolean(jobject["isPreferredCustomer"]));
            return customer;
        }
    }
}
