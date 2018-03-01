using Customers.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Logging
{
    public class ConsoleLogger : ILog
    {
        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }
    }
}
