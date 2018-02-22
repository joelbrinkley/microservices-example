using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers;

namespace Customers.UnitTests
{
    [TestClass]
    public class Customer_Should
    {
        private Name name = new Name("Ricky", "Morris", "Bobby");
        private Address address = new Address("154 street", null, "Franklins", "TN", "99933");
        private EmailAddress email = "testemail@gmail.com";

        [TestMethod]
        public void CreateNewCustomer()
        {
            var customer = Customer.Create(name, address, email);

            Assert.AreEqual(name, customer.Name);
            Assert.AreEqual(address, address);
            Assert.AreEqual(email, email);
        }
    }
}
