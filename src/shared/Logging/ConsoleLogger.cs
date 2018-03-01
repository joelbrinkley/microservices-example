using Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class ConsoleLogger : ILog
    {
        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message, Exception e)
        {
            Console.WriteLine(message);
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }
    }
}
