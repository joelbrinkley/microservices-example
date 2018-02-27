using AccountView.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountView.Api.ViewModels
{
    public class AccountViewModel
    {
        public static AccountViewModel CreateFor(Account account)
        {
            return new AccountViewModel();
        }
    }
}
