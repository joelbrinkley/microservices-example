using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILog
    {
        void Information(string message);
        void Information(string message, object context);
        void Error(string message);
        void Error(string message, Exception context);
    }
}
