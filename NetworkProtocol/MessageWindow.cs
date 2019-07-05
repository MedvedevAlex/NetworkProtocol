using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProtocol
{
    public class MessageWindow
    {
        private readonly int _size;

        public MessageWindow(int size)
        {
            _size = size;
        }

        public int HighBoundaryId { get { return _size; } }
        public int LowBoundaryId { get { return 1; } }
    }
}
