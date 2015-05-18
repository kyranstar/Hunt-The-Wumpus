using System;
using System.Linq;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// A class that handles inputs for the map.
    /// </summary>
    public class MapInputHandler
    {
        public const int PlayerVelocity = 600;
        private readonly GameController GameController;

        private KeyboardState KeyState;
        private bool IsFrozen
        {
            get
            {
                return GameController.CurrentTrivia != null;
            }
        }

        public MapInputHandler(GameController gameController)
        {
            this.GameController = gameController;
        }

        public bool IsAiming
        {
            get
            {
                if(IsFrozen)
                    return false;
                return KeyState.IsKeyDown(Keys.LeftShift);
            }
        }

        public Direction? NavDirection { get; protected set; }

        /// <summary>
        /// Handles all updates for the map this tick.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            KeyboardState NewKeyState = Keyboard.GetState();
            Keys[] OldKeys = KeyState.GetPressedKeys();
            Keys[] NewKeys = NewKeyState.GetPressedKeys();

            // Find all keys that are still pressed
            foreach (Keys Key in OldKeys.Intersect(NewKeys))
                HandleContinuedKeyPress(Key, time);

            // Find all keys that are no longer pressed
            foreach (Keys Key in OldKeys.Except(NewKeys))
                KeyUp(Key, time);

            // Find all keys that have been newly pressed
            foreach (Keys Key in NewKeys.Except(OldKeys))
                KeyDown(Key, time);

            // Update currently selected direction
            NavDirection = NewKeys.Select(k => MapKeyToDirection(k)).FirstOrDefault();

            KeyState = NewKeyState;
        }

        /// <summary>
        /// Handles map logic for the press of a single key
        /// </summary>
        /// <param name="Key"></param>
        private void KeyDown(Keys Key, GameTime GameTime)
        {
            Direction? KeyDirection = MapKeyToDirection(Key);

            if(!IsFrozen && !IsAiming && KeyDirection.HasValue)
                GameController.Map.MovePlayer(KeyDirection.Value);
            if (Key == Keys.Delete)
                GameController.CurrentTrivia.Abort();
        }

        /// <summary>
        /// Handles map logic for the press of a single key
        /// </summary>
        /// <param name="Key"></param>
        private void KeyUp(Keys Key, GameTime GameTime)
        {
            Direction? KeyDirection = MapKeyToDirection(Key);
            if (!IsFrozen && IsAiming && KeyDirection.HasValue)
                GameController.Map.TryShootTowards(KeyDirection.Value);
        }

        /// <summary>
        /// Called continuously as the key is held down.
        /// </summary>
        /// <param name="key"></param>
        private void HandleContinuedKeyPress(Keys key, GameTime GameTime)
        {
            // TODO: Do we want this, or should we just abandon it?
            if (!IsFrozen)
            {
                int SpeedIncrement = (PlayerVelocity * GameTime.ElapsedGameTime.TotalSeconds).ToInt();
                switch (key)
                {
                    case Keys.Up:
                        GameController.Map.PlayerRoomLocation.Y -= SpeedIncrement;
                        break;
                    case Keys.Down:
                        GameController.Map.PlayerRoomLocation.Y += SpeedIncrement;
                        break;
                    case Keys.Right:
                        GameController.Map.PlayerRoomLocation.X += SpeedIncrement;
                        break;
                    case Keys.Left:
                        GameController.Map.PlayerRoomLocation.X -= SpeedIncrement;
                        break;
                }
            }
        }

        private Direction? MapKeyToDirection(Keys Key)
        {
            switch (Key)
            {
                case Keys.W:
                    return Direction.North;
                case Keys.E:
                    return Direction.Northeast;
                case Keys.D:
                    return Direction.Southeast;
                case Keys.S:
                    return Direction.South;
                case Keys.A:
                    return Direction.Southwest;
                case Keys.Q:
                    return Direction.Northwest;
                default:
                    return null;
            }
        }
    }
}
