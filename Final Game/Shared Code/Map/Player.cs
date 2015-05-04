
using HuntTheWumpus.SharedCode.Scores;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// Represents the player. Holds his current stats like gold, arrows, etc.
    /// </summary>
    public class Player
    {
        private readonly ScoreEntry InternalScore;

        //Required fields and their getters
        public int Gold 
        {
            get { return InternalScore.GoldRemaining; }
            set { InternalScore.GoldRemaining = value; }
        }
        
        public int Arrows
        {
            get { return InternalScore.ArrowsRemaining; }
            set { InternalScore.ArrowsRemaining = value; }
        }

        public int Turns
        {
            get { return InternalScore.TurnsTaken; }
        }

        public int Score
        {
            get
            {
                return InternalScore.Score;
            }
        }

        public Player()
        {
            // TODO: Add default score params
            InternalScore = new ScoreEntry();
        }
    }
}
