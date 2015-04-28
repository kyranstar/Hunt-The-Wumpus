
namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// This class represents the Wumpus in a game.
    /// </summary>
    public class Wumpus
    {
        /// <summary>
        /// The current location of the Wumpus (Room ID)
        /// </summary>
        public int Location
        {
            set; get;
        }
        private Cave cave;

        /// <summary>
        /// Initializes a new instance of the Wumpus class.
        /// </summary>
        /// <param name="cave"></param>
        public Wumpus(Cave cave)
        {
            this.cave = cave;
        }
        /// <summary>
        /// Moves the Wumpus to a nearby valid position
        /// </summary>
        public void Move()
        {

        }
    }
}
