using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();
            var logMsg = "a";
            
            logger.Log(logMsg);

            Assert.That(logger.LastError, Is.EqualTo(logMsg));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidArgument_ThrowsArgumentException(string logMsg)
        {
            var logger = new ErrorLogger();
            
            Assert.That(() => logger.Log(logMsg), Throws.ArgumentNullException);
            // You could use this syntax for custom exception testing.
            // Assert.That(() => logger.Log(logMsg), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();

            var id = Guid.Empty;
            logger.ErrorLogged += (sender, args) =>
            {
                id = args;
            };
            
            logger.Log("a");
            
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}