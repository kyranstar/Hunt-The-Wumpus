
namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// Represents the player. Holds his current stats like gold, arrows, etc.
    /// </summary>
    public class Player
    {
        /*
        Since it has been a while since I used C#, any help with formatting would be nice!
        Most of this stuff is filler code for the specs, which say that the player must always
        be able to move, shoot arrows, purchase arrows and purchase a secret.
        
        As I've said before, any help with my code is greatly appreciated!
        
        The filler code here is strong.
        */
        
        //Required fields and their getters
        private int gold;
        public int Gold 
        {
            get { return gold; }
            set { arrows = value; }
        }
        
        private int arrows = 3;
        public int Arrows
        {
            get { return arrows; }
            set { arrows = value; }
        }
        
        private int score;
        public int Score
        {
            get { return score; }
        }
        

        private int turns;
        public int Turns
        {
            get { return turns; }
        }
        
        /// <summary>
        /// Currently filler code, we'll add on to this when we get there
        /// </summary>
        public void ShootArrow()
        {
            arrows--;
        }
        
        /// <summary>
        /// Also filler code, but if you beat the trivia, you get 2 arrows
        /// </summary>
        public void PurchaseArrows()
        {
            bool beatsTrivia = false;
            if (beatsTrivia)
            {
                arrows += 2;
            }
        }
        
        /// <summary>
        /// More filler code
        /// </summary>
        public void PurchaseSecret()
        {
            bool beatsTrivia = false;
            if (beatsTrivia)
            {
                //Obtains secret!
            }
        }
        
        /// <summary>
        /// Filler code is cool
        /// </summary>
        public void Move()
        {

        }

        /// <summary>
        /// A necessary method to be called by other classes. Should be useful to actively update.
        /// </summary>
        public void Update()
        {
            score = 100 - turns + gold + arrows;
        }
        
        /// <summary>
        /// A method a thought we would need, essentially incrementing stuff after a turn is made
        /// Also, adds the gold in the current room to the already existing amount of gold
        /// </summary>
        /// <param name="goldInRoom"></param>
        public void EntersRoom(int goldInRoom)
        {
            turns++;
            gold += goldInRoom;
            CheckHazards();
        }
        
        /// <summary>
        /// This method checks for hazards in the cave; Obtains this from the map/cave classes
        /// </summary>
        public void CheckHazards()
        {

        }
    }
}
