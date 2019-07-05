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
            MessageWindow window = new MessageWindow(size);
            Assert.That(window.LowBoundaryId, Is.EqualTo(low));
            Assert.That(window.HighBoundaryId, Is.EqualTo(high));

        }
    }
}
