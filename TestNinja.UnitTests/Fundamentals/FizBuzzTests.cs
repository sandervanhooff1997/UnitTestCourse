using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class FizBuzzTests
    {
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void GetOutput_ThreeAndFiveDivision_ReturnsFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        
        
        
        [Test]
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(9)]
        public void GetOutput_ThreeDivisionOnly_ReturnsFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        
        
        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        public void GetOutput_FiveDivisionOnly_ReturnsBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        
        [Test]
        [TestCase(2)]
        [TestCase(7)]
        [TestCase(11)]
        public void GetOutput_NoDivision_ReturnsNumber(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo(number.ToString()));
        }
    }
}