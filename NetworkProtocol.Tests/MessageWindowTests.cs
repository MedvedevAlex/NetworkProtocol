using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.That(window.LowBoundaryId, Is.EqualTo(low));
            Assert.That(window.HighBoundaryId, Is.EqualTo(high));

        }
        [Test]
        public void LowBoundaryIdIsRemoved_WindowIsShifted()
        {
            //Arrange
            MessageWindow window = new MessageWindow(15);
            //Act
            window.Remove(1);
            //Assert
            Assert.That(window.LowBoundaryId, Is.EqualTo(2));
            Assert.That(window.HighBoundaryId, Is.EqualTo(16));
        }
    }
}
