using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomListApp
{
    public class MyEnumerator<T> : IEnumerator<T>
    {
        public event Action Disposed;
        public MyEnumerator(T[] dataSet, int count)
        {
            _data = dataSet;
            _count = count;
        }
        public T Current
        {
            get
            {
                try
                {
                    return _data[_index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Disposed?.Invoke();
            return;
        }

        public bool MoveNext()
        {
            _index++;
            return (_index < _count);
        }

        public void Reset()
        {
            _index = -1;
        }

        private int _index = -1;
        private int _count;
        private T[] _data;
    }
}
