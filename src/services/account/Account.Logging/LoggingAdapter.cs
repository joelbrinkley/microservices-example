using Serilog;
using System;

namespace Account.Logging
{
    public class LoggingAdapter : ILog
    {
        public void Error(string message, Exception e)
        {
            Log.Error($"{message} \r\n {e.Message} \r\n {e.StackTrace}");
        }

        public void Information(string message)
        {
            Log.Information(message);
        }
    }
}
