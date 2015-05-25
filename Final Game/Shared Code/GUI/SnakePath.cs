using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class SnakePath
    {
        private Queue<Vector2> _Points;
        private int SnakeLength;

        public SnakePath(int snakeLength, Vector2 startPoint)
        {
            SnakeLength = snakeLength;

            _Points = new Queue<Vector2>();
            _Points.Enqueue(startPoint);
        }

        public SnakePath(int snakeLength)
            : this(snakeLength, new Vector2())
        {

        }
        
        public void AddPoint(Vector2 NewPoint)
        {
            _Points.Enqueue(NewPoint);

            if(_Points.Count > SnakeLength)
                _Points.Dequeue();
        }

        public Vector2[] Points
        {
            get
            {
                return _Points.ToArray();
            }
        }

        public Vector2 this[int index]
        {
            get
            {
                return Points[index];
            }
        }

        public Vector2 EarliestPoint
        {
            get
            {
                return _Points.Peek();
            }
        }

        public Vector2 LatestPoint
        {
            get
            {
                return this[_Points.Count - 1];
            }
        }

        public int Length
        {
            get
            {
                return SnakeLength;
            }
        }
    }
}
