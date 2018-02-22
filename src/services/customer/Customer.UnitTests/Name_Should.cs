using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Customers;

namespace Customers.UnitTests
{
    [TestClass]
    public class Name_Should
    {
        Name name = new Name("Ricky", "Morris", "Bobby");
      
        [TestMethod]
        public void ReturnFullName()
        {
            Assert.AreEqual(name.FullName, "Ricky Morris Bobby");
        }

        [TestMethod]
        public void ReturnFirstAndLast()
        {
            Assert.AreEqual(name.FirstAndLast, "Ricky Bobby");
        }

        [TestMethod]
        public void BeEqual()
        {
            Name name2 = new Name("Ricky", "Morris", "Bobby");
            Assert.IsTrue(name2.Equals(name));
        }

        [TestMethod]
        public void NotBeEqual()
        {
            Name name2 = new Name("Ryan", "Morris", "Bobby");
            Assert.IsFalse(name2.Equals(name));
        }
    }
}
