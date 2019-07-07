using NUnit.Framework;

namespace NetworkProtocol.Tests
{
    [TestFixture]
    public class MessageWindowTests
    {
        private MessageWindow _window;

        [SetUp]
        public void Setup()
        {
            _window = new MessageWindow(15);
        }

        [Test]
        public void LowAndHighBoundatiesInitiallySet()
        {
            //Arrange
            
            //Act

            //Assert
            AssertBoundariesValue(_window, 1, 15, 0);

        }

        [Test]
        public void LowBoundaryIdIsRemoved_WindowIsShifted()
        {
            //Arrange

            //Act
            _window.Remove(1);
            //Assert
            AssertBoundariesValue(_window, 2, 16, 0);
        }

        [Test]
        public void HighBoundaryIdIsRemoved_WindowIsNotShifted()
        {
            //Arrange
            
            //Act
            _window.Remove(15);
            //Assert
            AssertBoundariesValue(_window, 1, 15, 1);
        }

        [Test]
        public void MessageRemovedInsideWindow_BoundariesAreNotChanged()
        {
            //Arrange

            //Act
            _window.Remove(10);
            //Assert
            AssertBoundariesValue(_window, 1, 15, 1);
        }

        private void AssertBoundariesValue(MessageWindow window, int low, int high, int blocked)
        {
            Assert.That(window.LowBoundaryId, Is.EqualTo(low));
            Assert.That(window.HighBoundaryId, Is.EqualTo(high));
            Assert.That(_window.BlockedMessageAmount, Is.EqualTo(blocked));
        }
    }
}
