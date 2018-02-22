using Serilog;
using System;

namespace Account.Logging
{
    public class LoggingAdapter : ILog
    {
        public void Information(string message)
        {
            Log.Information(message);
        }
    }
}
