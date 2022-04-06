using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            var stack = new Stack<string>();
            
            Assert.That(stack.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Push_NullArgument_ThrowsArgumentNullException()
        {
            var stack = new Stack<string>();
            
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArgument_AddsObjectToList()
        {
            var stack = new Stack<string>();
            
            stack.Push("a");
            
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_EmptyList_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();
            
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_PopulatedList_ReturnsLastObject()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            
            var result = stack.Pop();
            
            Assert.That(result, Is.EqualTo("c"));          
        }
        
        [Test]
        public void Pop_PopulatedList_RemovesLastObject()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            
            stack.Pop();
            
            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyList_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }
        
        [Test]
        public void Peek_PopulatedList_ReturnsLastObject()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("c"));
        }
        
        [Test]
        public void Peek_PopulatedList_PreservesLastObject()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}