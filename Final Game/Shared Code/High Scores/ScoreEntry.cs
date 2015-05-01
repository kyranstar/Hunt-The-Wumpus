using System;
using System.Xml.Serialization;

namespace HuntTheWumpus.SharedCode.Scores
{
    public class ScoreEntry
    {
        /// <summary>
        /// The name that the user gave
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The number of turns that the player took to complete the game
        /// </summary>
        public int TurnsTaken { get; set; }

        /// <summary>
        /// The amount of gold remaining at the end of the game
        /// </summary>
        public int GoldRemaining { get; set; }

        /// <summary>
        /// The number of arrows remaining at the end of the game
        /// </summary>
        public int ArrowsRemaining { get; set; }

        /// <summary>
        /// The UTC time that this scoring game was completed
        /// </summary>
        public DateTime ScoreDate { get; set; }

        /// <summary>
        /// The time that this game was completed, in the local time zone.
        /// Should only be used for display to the user.
        /// </summary>
        [XmlIgnore]
        public DateTime LocalScoreDate
        {
            get
            {
                return ScoreDate.ToLocalTime();
            }
            set
            {
                ScoreDate = value.ToUniversalTime();
            }
        }

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
