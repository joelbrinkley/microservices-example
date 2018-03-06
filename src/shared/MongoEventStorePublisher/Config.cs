using System;
using System.Collections.Generic;
using System.Text;

namespace MongoEventStorePublisher
{
    public class Config
    {
        public static string MONGODB
        {
            get
            {
                return Environment.GetEnvironmentVariable("MONGODB");
            }
        }

        public static string BROKER_URL
        {
            get
            {
                return Environment.GetEnvironmentVariable("MESSAGE_QUEUE_URL");
            }
        }

        public static string ELK_URL
        {
            get
            {
                return Environment.GetEnvironmentVariable("ELK");
            }
        }
    }
}
