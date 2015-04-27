using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GUI;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HuntTheWumpus.SharedCode.Scores
{
    [Serializable]
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
