using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
    }
}
