using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product() {ListPrice = 100};

            var goldCustomer = new Customer() {IsGold = true};
            var result = product.GetPrice(goldCustomer);
            
            Assert.That(result, Is.EqualTo(70));
        }
    }
}