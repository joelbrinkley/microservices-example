using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(Guid customerId): base($"Customer {customerId} is not found.")
        {

        }
    }
}
