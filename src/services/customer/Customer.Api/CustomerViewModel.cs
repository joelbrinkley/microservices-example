using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Api
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public bool IsPreferredCustomer { get; set; }

        public CustomerViewModel(string name, bool isPreferredCustomer)
        {
            Name = name;
            IsPreferredCustomer = isPreferredCustomer;
        }
    }
}
