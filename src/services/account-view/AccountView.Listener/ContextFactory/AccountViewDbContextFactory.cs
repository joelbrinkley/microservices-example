using System;
using System.Collections.Generic;
using System.Text;
using AccountView.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountView.Listener.ContextFactory
{
    public class AccountViewDbContextFactory : IAccountViewContextFactory
    {
        private readonly DbContextOptions options;

        public AccountViewDbContextFactory(DbContextOptions options)
        {
            this.options = options;
        }

        public AccountViewContext GetContext()
        {
            return new AccountViewContext(options);
        }
    }
}
