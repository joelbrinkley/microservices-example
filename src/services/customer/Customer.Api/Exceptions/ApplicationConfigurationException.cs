using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Exceptions
{
    public class ApplicationConfigurationException : Exception
    {
        public ApplicationConfigurationException(string message) : base(message)
        {

        }
    }
}
