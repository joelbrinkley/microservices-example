using Domain.Aggregates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.UnitTests
{
    [TestClass]
    public class AggregateId
    {
        [TestMethod]
        public void AssignsValue()
        {
            var aggregateId = new AggregateId<int>(5);
            Assert.AreEqual(5, aggregateId.Value);
        }

        [TestMethod]
        public void ImplicitAssignmentToType()
        {
            AggregateId<int> aggregateId = 5;
            Assert.AreEqual(5, aggregateId.Value);
        }
    }
}
