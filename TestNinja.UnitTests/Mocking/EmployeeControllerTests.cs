using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            var storage = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(storage.Object);

            var response = controller.DeleteEmployee(1);
            
            // Verify the interaction between Controller and Storage, by checking if the DeleteEmployee method was called with the correct parameter.
            storage.Verify(s => s.DeleteEmployee(1));
            Assert.That(response, Is.TypeOf<RedirectResult>());
        }
    }
}