using System;
using System.Collections.Generic;
using System.Text;

namespace Customers
{
    public class Address
    {
        public Address(string addressline1, string addressLine2, string city, string state, string zipcode)
        {
            Addressline1 = addressline1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            Zipcode = zipcode;
        }

        public string Addressline1 { get; }
        public string AddressLine2 { get; }
        public string City { get; }
        public string State { get; }
        public string Zipcode { get; }
    }
}
