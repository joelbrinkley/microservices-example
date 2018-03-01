using AccountView.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener.ContextFactory
{
    public interface IAccountViewContextFactory
    {
        AccountViewContext GetContext();
    }
}
