using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Listener
{
    public class Config
    {
        public static string MONGO_CONNECTION
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

        public static string CUSTOMER_URL
        {
            get
            {
                return Environment.GetEnvironmentVariable("CUSTOMER_URL");
            }
        }
    }
}
