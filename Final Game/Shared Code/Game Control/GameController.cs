using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scenes;
using HuntTheWumpus.SharedCode.Trivia;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GameControl
{
    public class GameController
    {
        public TriviaSet CurrentTrivia;
        public TriviaQuestionState QuestionState;
        public TriviaSet.QuestionUpdateHandler NewQuestionHandler;
        public readonly Map Map;

        public GameController()
        {
            Map = new Map();
            Map.OnPlayerMoved += Map_PlayerMoved;
        }

        public void LoadNewTrivia(TriviaQuestionState triviaType, int numTriviaQuestions)
        {
            if (CurrentTrivia != null && !CurrentTrivia.IsComplete)
                Log.Warn("New trivia set added before previous set was complete!");

            CurrentTrivia = Trivia.Trivia.CreateTriviaSet(numTriviaQuestions, NewQuestionHandler);
            QuestionState = triviaType;
            NewQuestionHandler(CurrentTrivia, new EventArgs());
        }

        public void CloseTrivia()
        {
            CurrentTrivia = null;
            QuestionState = TriviaQuestionState.None;
        }

        public void Initialize()
        {
            // Ideally, the Map should have a reset method
            // TODO: Reset map here

            Map.Cave.RegenerateLayout();
        }

        public void Update(GameTime GameTime)
        {
            ResolveRunningTrivia();

            Map.Update(GameTime);
            // TODO: Stop running input handler while trivia is active
        }

        private void ResolveRunningTrivia()
        {
            if (CurrentTrivia != null
                && CurrentTrivia.IsComplete)
            {
                if (QuestionState == TriviaQuestionState.TrappedInPit)
                    ResolvePitTrivia();
                else if (QuestionState == TriviaQuestionState.FightingWumpus)
                {
                    // TODO: Handle wumpus fight trivia as well
                }

                CloseTrivia();
            }
        }

        private void ResolvePitTrivia()
        {
            if (CurrentTrivia.NumberCorrect >= 2)
            {
                Func<Room, bool> RoomValidator = r =>
                    !r.HasBats
                    && !r.HasPit
                    && Map.Wumpus.Location != r.RoomID
                    && Map.PlayerRoom != r.RoomID;

                // Place player in already visited location without hazards
                Room FirstSafeRoomInPlayerPath = Map.PlayerPath.Select(i => Map.Cave[i])
                    .FirstOrDefault(RoomValidator);

                // If there are no non-hazardous locations that we've already visited
                if (FirstSafeRoomInPlayerPath == null)
                {
                    // Check all possible rooms instead
                    var FirstSafeRoom = Map.Cave.Rooms
                        .FirstOrDefault(RoomValidator);

                    // TODO: This will increment the player's turn
                    // count by two when they hit a pit (once to fall
                    // into the pit, once to move to their origin).
                    // Is this what we want?
                    if (FirstSafeRoom != null)
                        Map.MovePlayerTo(FirstSafeRoom.RoomID);
                }
                else
                    // TODO: Same as above
                    Map.MovePlayerTo(FirstSafeRoomInPlayerPath.RoomID);
            }
            else
            {
                // TODO: End game?
            }
        }

        public void Map_PlayerMoved(object sender, EventArgs e)
        {
            if (Map.PlayerRoom == Map.Wumpus.Location)
            {
                LoadNewTrivia(TriviaQuestionState.FightingWumpus, 5);
            }
            else if (Map.Cave[Map.PlayerRoom].HasPit)
            {
                LoadNewTrivia(TriviaQuestionState.TrappedInPit, 3);
            }
        }
    }
}
