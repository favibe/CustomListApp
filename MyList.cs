using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomListApp
{
    public class MyList<T> : IList<T>
    {
        public MyList(int capacity = 0)
        {
            _data = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                return _data[index];
            }
            set
            {
                if (_currentEnumerator != null)
                    throw new InvalidOperationException(LIST_CHANGING_ERROR);

                if (index >= _count)
                    throw new ArgumentOutOfRangeException();

                _data[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_currentEnumerator != null)
                throw new InvalidOperationException(LIST_CHANGING_ERROR);

            _count++;

            if (_count >= _data.Length)
            {
                Resize();
            }

            _data[_count - 1] = item;

        }

        public void Clear()
        {
            if (_currentEnumerator != null)
                throw new InvalidOperationException(LIST_CHANGING_ERROR);

            _count = 0;
            _data = new T[0];
        }

        public bool Contains(T item)
        {
            if (IndexOf(item) != -1)
                return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < _count; i++)
            {
                array.SetValue(_data[i], arrayIndex++);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            _currentEnumerator = new MyEnumerator<T>(_data, _count);
            _currentEnumerator.Disposed += OnDisposeEnumerator;

            return _currentEnumerator;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (_currentEnumerator != null)
                throw new InvalidOperationException(LIST_CHANGING_ERROR);

            if (index > Count - 1 || index < 0)
                throw new ArgumentOutOfRangeException();

            _count++;

            if (_count >= _data.Length)
            {
                Resize();
            }

            for (int i = Count - 1; i > index; i--)
            {
                _data[i] = _data[i - 1];
            }

            _data[index] = item;
        }

        public bool Remove(T item)
        {
            if (_currentEnumerator != null)
                throw new InvalidOperationException(LIST_CHANGING_ERROR);

            int index = IndexOf(item);

            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count))
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _data[i] = _data[i + 1];
                }
                _count--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnDisposeEnumerator()
        {
            _currentEnumerator.Disposed -= OnDisposeEnumerator;
            _currentEnumerator = null;
        }

        private void Resize()
        {
            var temp = _data;

            _data = new T[_count * 2];

            Array.Copy(temp, _data, temp.Length);
        }

        private T[] _data;
        private int _count;

        private const string LIST_CHANGING_ERROR = "List changing is not allowed, while enumerator exist";

        private MyEnumerator<T> _currentEnumerator;
    }
}
