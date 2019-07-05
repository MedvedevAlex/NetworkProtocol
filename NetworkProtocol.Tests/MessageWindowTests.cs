using NUnit.Framework;

namespace NetworkProtocol.Tests
{
    [TestFixture]
    public class MessageWindowTests
    {
        [TestCase(10,1,10)]
        [TestCase(20,1,20)]
        public void LowAndHighBoundatiesInitiallySet(int size, int low, int high)
        {
            //Arrange
            MessageWindow window = new MessageWindow(size);
            //Act

            //Assert
            AssertBoundariesValue(window, low, high);

        }

        [TestCase(15, 1, 15)]
        public void LowBoundaryIdIsRemoved_WindowIsShifted(int size, int low, int high)
        {
            //Arrange
            MessageWindow window = new MessageWindow(size);
            //Act
            window.Remove(1);
            //Assert
            AssertBoundariesValue(window, 2, 16);
        }

        [TestCase(15, 1, 15)]
        public void HighBoundaryIdIsRemoved_WindowIsNotShifted(int size, int low, int high)
        {
            //Arrange
            MessageWindow window = new MessageWindow(size);
            //Act
            window.Remove(high);
            //Assert
            AssertBoundariesValue(window, 1, 14);
        }

        private void AssertBoundariesValue(MessageWindow window, int low, int high)
        {
            Assert.That(window.LowBoundaryId, Is.EqualTo(low));
            Assert.That(window.HighBoundaryId, Is.EqualTo(high));
        }
    }
}
