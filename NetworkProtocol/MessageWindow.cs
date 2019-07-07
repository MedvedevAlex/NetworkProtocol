using System;
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
            if (HighBoundaryId < id)
            {
                throw new AttemptToRemoveIdAboveHighBoundary(HighBoundaryId, id);
            }

            if (id < LowBoundaryId)
            {
                throw new AttemptToRemoveIdUnderLowBoundary(LowBoundaryId, id);
            }

            if (!_window.Contains(id))
            {
                throw new AttemptToRemoveIdThatHasBeenAlreadyRemoved_ExceptionRaises(id);
            }

            int minId = LowBoundaryId;

            _window.Remove(id);

            if (minId == id)
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

        public class AttemptToRemoveIdAboveHighBoundary : Exception
        {
            public AttemptToRemoveIdAboveHighBoundary(int high, int id)
                : base($"high boundary id: {high}, got: {id}") { }
        }

        public class AttemptToRemoveIdUnderLowBoundary : Exception
        {
            public AttemptToRemoveIdUnderLowBoundary(int low, int id)
                : base($"low boundary id: {low}, got: {id}") { }
        }

        public class AttemptToRemoveIdThatHasBeenAlreadyRemoved_ExceptionRaises : Exception
        {
            public AttemptToRemoveIdThatHasBeenAlreadyRemoved_ExceptionRaises(int id)
                : base($"id has been already removed: {id}") { }
        }
    }
}
