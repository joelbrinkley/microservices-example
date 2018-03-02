using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener
{
    public class Config
    {
        public static string SqlConnection
        {
            get
            {
                return Environment.GetEnvironmentVariable("SQL_CONNECTION");
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
