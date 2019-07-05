using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProtocol
{
    public class MessageWindow
    {
        public int HighBoundaryId => _window[_window.Count - 1];
        public int LowBoundaryId => _window[0];

        private readonly List<int> _window;
        private readonly int _size;

        public MessageWindow(int size)
        {
            _size = size;
            _window = new List<int>(size);
            FillWindow();
        }

        public void Remove(int id)
        {
            _window.Remove(id);
            ShiftWindow();
        }

        private void FillWindow()
        {
            for (int i = 1; i <= _size; i++)
            {
                _window.Add(i);
            }
        }

        private void ShiftWindow()
        {
            _window.Add(LowBoundaryId + _size - 1);
        }
    }
}
