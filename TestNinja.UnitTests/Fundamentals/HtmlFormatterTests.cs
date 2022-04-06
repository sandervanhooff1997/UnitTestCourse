using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();
            var content = "abc";

            var result = formatter.FormatAsBold(content);
            
            // Specific assert (ignore case)
            Assert.That(result, Is.EqualTo($"<strong>{content}</strong>").IgnoreCase);
            
            // More general assert
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain(content));
        }
    }
}