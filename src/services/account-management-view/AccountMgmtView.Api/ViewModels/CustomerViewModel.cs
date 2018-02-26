using AccountMgmtView.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMgmtView.Api.ViewModels
{
    public class CustomerViewModel
    {
        public static CustomerViewModel CreateFor(Customer customer)
        {
            return new CustomerViewModel();
        }
    }
}
