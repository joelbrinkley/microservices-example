using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Logging
{
    public interface ILog
    {
        void Information(string message);
        void Error(string message, Exception e);
    }
}
