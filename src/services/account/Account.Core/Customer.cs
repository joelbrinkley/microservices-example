using System;
using System.Collections.Generic;
using System.Text;

namespace Account
{
    public class Customer
    {
        public Customer(string name, bool isPreferredCustomer)
        {
            Name = name;
            IsPreferredCustomer = isPreferredCustomer;
        }

        public string Name { get; }
        public bool IsPreferredCustomer { get; }
    }
}
