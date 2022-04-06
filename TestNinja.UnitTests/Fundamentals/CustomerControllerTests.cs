using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFound()
        {
            var controller = new CustomerController();

            var response = controller.GetCustomer(0);
            
            Assert.That(response, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnsOk()
        {
            var controller = new CustomerController();

            var response = controller.GetCustomer(1);
            
            Assert.That(response, Is.TypeOf<Ok>());
        }
    }
}