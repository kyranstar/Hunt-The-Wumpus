using System;
using System.Collections.Generic;
using System.Linq;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Trivia;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.SharedCode.GameMap
{

    /// <summary>
    ///     An enumeration of hexagonal directions
    /// </summary>
    public enum Direction
    {
        North,
        Northeast,
        Southeast,
        South,
        Southwest,
        Northwest
    }

    /// <summary>
    ///     Represents warnings to be given to the player if they are too close to something.
    /// </summary>
    public enum PlayerWarnings
    {
        /// <summary>
        ///     Represents when the player is within one tile of a pit
        /// </summary>
        Pit,

        /// <summary>
        ///     Represents when the player is within one tile of a bat
        /// </summary>
        Bat,

        /// <summary>
        ///     Represents when the player is within one (maybe more for the wumpus? we should discuss this) tile of the wumpus
        /// </summary>
        Wumpus
    }

    /// <summary>
    ///     An enumeration of square directions
    /// </summary>
    public enum SquareDirection
    {
        North,
        East,
        South,
        West
    }
}