using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scenes;
using HuntTheWumpus.SharedCode.Trivia;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        public void Initialize()
        {
            // Ideally, the Map should have a reset method
            // TODO: Reset map here

            Map.Cave.RegenerateLayout();
        }

        public void Update(GameTime GameTime)
        {
            Map.Update(GameTime);
        }

        public void Map_PlayerMoved(object sender, EventArgs e)
        {
            if (Map.PlayerRoom == Map.Wumpus.Location)
            {
                LoadNewTrivia(TriviaQuestionState.FightingWumpus, 5);
                // TODO: Stop running input handler
            }
        }
    }
}
