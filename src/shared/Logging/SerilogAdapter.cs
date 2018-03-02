using Newtonsoft.Json;
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

        public void Error(string message, Exception exception)
        {
            Log.Information($"{message} \r\n {exception.Message} \r\n {exception.StackTrace}");
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Information(string message, object context)
        {
            var json = JsonConvert.SerializeObject(context, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Log.Information($"{message} \r\n {json}");
        }
    }
}
