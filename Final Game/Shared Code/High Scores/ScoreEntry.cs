using System;
using System.Xml.Serialization;

namespace HuntTheWumpus.SharedCode.Scores
{
    public class ScoreEntry
    {
        public string Username { get; set; }

        public int TurnsTaken { get; set; }
        public int GoldRemaining { get; set; }
        public int ArrowsRemaining { get; set; }

        [XmlIgnore]
        public int Score
        {
            get
            {
                return 100 - TurnsTaken + GoldRemaining + (10 * ArrowsRemaining);
            }
        }

    }
}
