
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
        NoArrows,
        NoGold,
    }
}
