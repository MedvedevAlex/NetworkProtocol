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
            AssertBoundariesValue(1, 15, 0);
        }

        [Test]
        public void LowBoundaryIdIsRemoved_WindowIsShifted()
        {
            //Arrange

            //Act
            _window.Remove(1);
            //Assert
            AssertBoundariesValue(2, 16, 0);
        }

        [Test]
        public void MessageRemovedInsideWindow_BoundariesAreNotChanged()
        {
            //Arrange

            //Act
            _window.Remove(15);
            _window.Remove(10);
            //Assert
            AssertBoundariesValue(1, 15, 2);
        }

        [Test]
        public void MessageRemovedInside_LowBoundaryRemoved_WindowIsShiftedAndTakesIntoAccountBlockedAmount()
        {
            //Arrange

            //Act
            _window.Remove(10);
            _window.Remove(1);
            //Assert
            AssertBoundariesValue(2, 16, 1);
        }

        [Test]
        public void MessageRemovedInside_LowBoundaryRemovedSeveralTimes_WindowIsShiftedAndTakesIntoAccountBlockedAmount()
        {
            //Arrange

            //Act
            _window.Remove(2);
            _window.Remove(3);
            _window.Remove(4);

            _window.Remove(6);
            _window.Remove(7);
            _window.Remove(8);

            //Assert
            AssertBoundariesValue(1, 15, 6);

            _window.Remove(1);
            AssertBoundariesValue(5, 19, 3);

            _window.Remove(5);
            AssertBoundariesValue(9, 23, 0);
        }

        [Test]
        public void WindowIsShifted_AddedValuesContinueTheSequence()
        {
            //Arrange

            //Act
            for (int i = 1; i <= 15; i++)
            {
                _window.Remove(i);
            }

            //Assert
            AssertBoundariesValue(16, 30, 0);
        }

        [Test]
        public void AttemptToRemoveIdAboveHighBoundary_ExceptionRaises()
        {
            //Arrange

            //Act

            //Assert
            Assert.That(() => _window.Remove(20),
                Throws.Exception.TypeOf<MessageWindow.AttemptToRemoveIdAboveHighBoundary>()
                .With.Message.EqualTo("high boundary id: 15, got: 20"));
        }

        [Test]
        public void AttemptToRemoveIdUnderLowBoundary_ExceptionRaises()
        {
            //Arrange

            //Act

            //Assert
            Assert.That(() => _window.Remove(0),
                Throws.Exception.TypeOf<MessageWindow.AttemptToRemoveIdUnderLowBoundary>()
                .With.Message.EqualTo("low boundary id: 1, got: 0"));
        }

        [Test]
        public void AttemptToRemoveIdThatHasBeenAlreadyRemoved_ExceptionRaises()
        {
            //Arrange

            //Act
            _window.Remove(5);

            //Assert
            Assert.That(() => _window.Remove(5),
                Throws.Exception.TypeOf<MessageWindow.AttemptToRemoveIdThatHasBeenAlreadyRemoved_ExceptionRaises>()
                .With.Message.EqualTo("id has been already removed: 5"));
        }

        private void AssertBoundariesValue(int low, int high, int blocked)
        {
            Assert.That(_window.LowBoundaryId, Is.EqualTo(low));
            Assert.That(_window.HighBoundaryId, Is.EqualTo(high));
            Assert.That(_window.BlockedMessageAmount, Is.EqualTo(blocked));
        }
    }
}
