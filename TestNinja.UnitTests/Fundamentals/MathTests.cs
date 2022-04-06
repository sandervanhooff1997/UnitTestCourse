using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;
        
        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [TearDown]
        public void TearDown()
        {
            
        }
        
        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        public void Max_WhenCalled_ReturnsGreaterNumber(int a, int b, int expected)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Ignore("Just because I want to!")]
        public void Min_WhenCalled_ReturnsLowestNumber()
        {
            
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);
            
            // Assert.That(result, Is.Not.Empty);
            // Assert.That(result.Count(), Is.EqualTo(3));
            
            // Assert.That(result, Does.Contain(1));
            // Assert.That(result, Does.Contain(3));
            // Assert.That(result, Does.Contain(5));
            
            Assert.That(result, Is.EquivalentTo(new [] {1,3,5 }));
             
            // Bonus
            // Assert.That(result, Is.Ordered);
            // Assert.That(result, Is.Unique);
        }
        
        [Test]
        public void GetOddNumbers_LimitIsLowerThanZero_ReturnOddNumbersUpToLimit()
        {
            
        }
    }
}