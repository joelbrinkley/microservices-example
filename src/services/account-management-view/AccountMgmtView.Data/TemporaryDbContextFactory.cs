using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountMgmtView.Data
{
    /*
     * https://benjii.me/2016/06/entity-framework-core-migrations-for-class-library-projects/
     */

    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<AccountMgmtViewContext>
    {   
        public AccountMgmtViewContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AccountMgmtViewContext>();
            builder.UseSqlServer("Server=localhost,1515;Database=AccountMgmtView;User=sa;Password=PWord1!!!;");
            return new AccountMgmtViewContext(builder.Options);
        }
    }
}
