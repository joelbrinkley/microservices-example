using Serilog;
using System;

namespace Customers.Logging
{
    public class LoggingAdapter : ILog
    {
        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }
    }
}
