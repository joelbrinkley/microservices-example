using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.UnitTests
{
    [TestClass]
    public class CustomerBenefits_Should
    {
        [TestMethod]
        public void Apply500BonusFundsWhenWhenAPreferedCustomerOpensAnAccount()
        {
            var customer = new Customer("John Moore", true);
            var startingFunds = CustomerBenefits.CalculateOpeningAccountBalance(customer, 100);
            Assert.AreEqual(600, startingFunds);
        }

        [TestMethod]
        public void NotApplyBonusFunds()
        {
            var customer = new Customer("John Moore", false);
            var startingFunds = CustomerBenefits.CalculateOpeningAccountBalance(customer, 100);
            Assert.AreEqual(100, startingFunds);
        }
    }
}
