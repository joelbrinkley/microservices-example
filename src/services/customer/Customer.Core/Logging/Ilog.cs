using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Logging
{
    public interface ILog
    {
        void Information(string message);
        void Error(string message);
    }
}
