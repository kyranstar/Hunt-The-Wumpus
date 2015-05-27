using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scenes;
using HuntTheWumpus.SharedCode.Scores;
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
        public delegate void GameOverHandler(object sender, EventArgs e);
        public event GameOverHandler OnGameOver;

        public delegate void NewHintHandler(object sender, EventArgs e);
        public event NewHintHandler OnNewHintAvailable;

        public delegate void NewSecretHandler(object sender, EventArgs e);
        public event NewSecretHandler OnNewSecretAvailable;

        /// <summary>
        /// Stores information about the game once it has concluded.
        /// This will be <code>null</code> until the game ends.
        /// </summary>
        public GameOverState GameOverState = null;

        public TriviaSet CurrentTrivia;
        public TriviaQuestionState QuestionState;
        public TriviaSet.QuestionUpdateHandler OnNewQuestion;

        public readonly Map Map;
        public readonly MapInputHandler InputHandler;

        private SecretManager SecretMan;

        public GameController()
        {
            Map = new Map();
            InputHandler = new MapInputHandler(this);
            SecretMan = new SecretManager(this);
            Map.OnPlayerMoved += Map_PlayerMoved;
        }


        public void Reset()
        {
            Map.Reset();
            GameOverState = null;
            CloseTrivia();
        }


        public void LoadNewTrivia(TriviaQuestionState triviaType, int numTriviaQuestions)
        {
            if (CurrentTrivia != null && !CurrentTrivia.IsComplete)
                Log.Warn("New trivia set added before previous set was complete!");

            CurrentTrivia = Trivia.Trivia.CreateTriviaSet(numTriviaQuestions, OnNewQuestion);
            QuestionState = triviaType;
            OnNewQuestion(CurrentTrivia, new EventArgs());
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

            InputHandler.Update(GameTime);
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
                else if (QuestionState == TriviaQuestionState.HitWumpus)
                    ResolveWumpusCollisionTrivia();
                else if (QuestionState == TriviaQuestionState.PurchasingSecret)
                    ResolveSecretTrivia();
                else if (QuestionState == TriviaQuestionState.PurchasingArrow)
                    ResolveArrowTrivia();

                CloseTrivia();
            }
        }

        private void ResolveSecretTrivia()
        {
            if (CurrentTrivia.NumberCorrect >= 2)
            {
                SecretMan.UnlockNewSecret();
                RaiseNewSecret();
            }
            else
                // TODO: Notify the user
                Log.Info("No secret for you.");
        }
        
        private void ResolveArrowTrivia()
        {
            if (CurrentTrivia.NumberCorrect >= 2)
            {
                Map.Player.Arrows++;
            }
            else
                // TODO: Notify the user
                Log.Info("No arrow for you.");
        }


        private void ResolvePitTrivia()
        {
            if (CurrentTrivia.NumberCorrect >= 3)
            {
                Room FirstSafeRoom = Map.GetPlayerStartRoom();

                // TODO: This will increment the player's turn
                // count by two when they hit a pit (once to fall
                // into the pit, once to move to their origin).
                // Is this what we want?
                if (FirstSafeRoom != null)
                    Map.MovePlayerTo(FirstSafeRoom.RoomID);
                else
                    Log.Error("Couldn't find valid room to move player to!");
            }
            else
            {
                EndGame(GameOverCause.FellInPit);
            }
        }

        private void ResolveWumpusCollisionTrivia()
        {
            if (CurrentTrivia.NumberCorrect >= 3)
            {
                Map.Wumpus.HitPlayer();
            }
            else
            {
                EndGame(GameOverCause.HitWumpus);
            }
        }

        public void EndGame(GameOverCause Cause)
        {
            GameOverState = new GameOverState()
            {
                Cause = Cause,
                PlayerScore = Map.Player.ScoreEntry,
                WonGame = Cause == GameOverCause.ShotWumpus
            };

            RaiseGameOver();
        }

        private void RaiseGameOver()
        {
            if (OnGameOver != null)
                OnGameOver(this, new EventArgs());
        }

        private void RaiseNewHint()
        {
            if (OnNewHintAvailable != null)
                OnNewHintAvailable(this, new EventArgs());
        }

        private void RaiseNewSecret()
        {
            if (OnNewSecretAvailable != null)
                OnNewSecretAvailable(this, new EventArgs());
        }

        public void Map_PlayerMoved(object sender, EventArgs e)
        {
            if (Map.PlayerRoom == Map.Wumpus.Location)
            {
                LoadNewTrivia(TriviaQuestionState.HitWumpus, 5);
            }
            else if (Map.Cave[Map.PlayerRoom].HasPit)
            {
                LoadNewTrivia(TriviaQuestionState.TrappedInPit, 3);
            }
        }

        public bool TryShootTowards(Direction direction)
        {
            // TODO: Animate shooting
            // TODO: Verify arrow count (before and after)

            Map.Player.Arrows--;

            int targetRoom = Map.Cave[Map.PlayerRoom].AdjacentRooms[(int)direction];
            if (Map.CanShootTo(targetRoom))
            {
                if (Map.Wumpus.Location == targetRoom)
                {
                    // TODO: end game
                    Log.Info("Yay! You shot the wumpus!");
                    EndGame(GameOverCause.ShotWumpus);
                    return true;
                }
                // TODO: Present message (miss)
                Log.Info("Your arrow missed the wumpus.");
                return false;
            }
            // TODO: Present message (hit wall)
            Log.Info("You managed to shoot a wall. Good job.");
            return false;
        }
    }
}
