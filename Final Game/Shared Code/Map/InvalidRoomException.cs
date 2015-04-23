using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GameMap
{
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
