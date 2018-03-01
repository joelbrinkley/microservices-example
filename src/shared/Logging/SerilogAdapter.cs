using Serilog;
using System;

namespace Logging
{
    public class SerilogAdapter : ILog
    {
        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception e)
        {
            Log.Error($"{message} \r\n \r\n {e.Message} \r\n {e.StackTrace}");
        }

        public void Information(string message)
        {
            Log.Information(message);
        }
    }
}
