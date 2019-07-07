using System.Collections.Generic;

namespace NetworkProtocol
{
    public class MessageWindow
    {
        public int HighBoundaryId => LowBoundaryId + _size - 1;
        public int LowBoundaryId => _window[0];
        public int BlockedMessageAmount => _size - _window.Count;

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
            int minId = LowBoundaryId;

            _window.Remove(id);

            if(minId == id)
                ShiftWindow(id);
        }

        private void FillWindow()
        {
            for (int i = 1; i <= _size; i++)
            {
                _window.Add(i);
            }
        }

        private void ShiftWindow(int id)
        {
            for (int i = id; i < LowBoundaryId; i++)
            {
                _window.Add(id + _size);
            }
        }
    }
}
