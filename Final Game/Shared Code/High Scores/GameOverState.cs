using System;
using System.Xml.Serialization;

namespace HuntTheWumpus.SharedCode.Scores
{
    public class GameOverState
    {
        public GameOverCause Cause;
        public ScoreEntry PlayerScore;
        public bool WonGame;
    }

    public enum GameOverCause
    {
        FellInPit,
        HitWumpus,
        ShotWumpus,
        NoArrows
        // TODO: What other situations end the game? Running out of gold?
    }
}
