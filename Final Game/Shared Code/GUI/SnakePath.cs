using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A FIFO queue with a limit on how many elements it can hold.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoundedQueue<T>
    {
        private readonly Queue<T> _values;
        private readonly int _length;

        public BoundedQueue(int length, T startPoint)
            : this(length)
        {
            _values.Enqueue(startPoint);
        }
        public BoundedQueue(int length)
        {
            _length = length;
            _values = new Queue<T>();
        }
        /// <summary>
        /// Enqueues and dequeues if there are too many values
        /// </summary>
        /// <param name="newValue"></param>
        public void Enqueue(T newValue)
        {
            _values.Enqueue(newValue);

            if (_values.Count > _length)
                _values.Dequeue();
        }
        /// <summary>
        /// Values as an array
        /// </summary>
        public T[] Values
        {
            get
            {
                return _values.ToArray();
            }
        }

        public T this[int index]
        {
            get
            {
                return Values[index];
            }
        }
        /// <summary>
        /// Returns the first value in the queue
        /// </summary>
        public T EarliestValue
        {
            get
            {
                return _values.Peek();
            }
        }
        /// <summary>
        /// Returns the last value in the queue
        /// </summary>
        public T LatestValue
        {
            get
            {
                return this[_values.Count - 1];
            }
        }

        public int Length
        {
            get
            {
                return _length;
            }
        }
        public bool Contains(T val)
        {
            return this._values.Contains(val);
        }
        public void Clear()
        {
            this._values.Clear();
        }
    }
    /// <summary>
    /// A path representing a bounded queue of points
    /// </summary>
    public class SnakePath : BoundedQueue<Vector2>
    {
        public SnakePath(int pathLength, Vector2 startPoint)
            : base(pathLength, startPoint)
        {
        }

    }
}
