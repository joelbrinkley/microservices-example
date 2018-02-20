using System;
using System.Collections.Generic;
using System.Text;

namespace Account
{
    public class InsufficentFundsException : Exception
    {
        public InsufficentFundsException() : base("Insufficent funds. Cannot make withdraw.")
        {

        }
    }
}
