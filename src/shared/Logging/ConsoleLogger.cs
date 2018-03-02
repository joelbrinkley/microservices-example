using Logging;
using Newtonsoft.Json;
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

        public void Error(string message, Exception exception)
        {

            Console.WriteLine($"{message} \r\n {exception.Message} \r\n {exception.StackTrace}");
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }

        public void Information(string message, object context)
        {
            var json = JsonConvert.SerializeObject(context, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Console.WriteLine($"{message} \r\n {json}");
        }
    }
}
