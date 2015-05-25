using System;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// An exception thrown when a room is invalid.
    /// </summary>
    class InvalidRoomException : Exception
    {
        public InvalidRoomException()
        {
        }

        public InvalidRoomException(string message)
            : base(message)
        {
        }

        public InvalidRoomException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
