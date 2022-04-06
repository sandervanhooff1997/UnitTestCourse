using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    /// <summary>
    /// This is a interaction test.
    /// Note: interaction tests ONLY test the interaction between objects, NOT the individual implementation.
    /// </summary>
    [TestFixture]
    public class OrderServiceTests
    {
        [Test(Author = "Sander van Hooff", Description = "Tests the interaction between OrderService and IStorage by verifying if IStorage.Store() was called.")]
        public void PlaceOrder_WhenCalled_ShouldStoreOrder()
        {
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            var order = new Order();
            orderService.PlaceOrder(order);
            
            storage.Verify(s => s.Store(order));
        }
    }
}