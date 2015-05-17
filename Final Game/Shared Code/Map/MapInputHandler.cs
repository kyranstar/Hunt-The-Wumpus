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
        private readonly Map map;
        private Keys[] PressedKeys = {};

        public MapInputHandler(Map map)
        {
            this.map = map;
        }

        public bool IsAiming
        {
            get
            {
                return PressedKeys.Contains(Keys.LeftShift);
            }
        }

        public Direction? NavDirection
        {
            get
            {
                // TODO: Clean up this logic
                Direction? Dir;
                foreach (Keys Key in PressedKeys)
                    if ((Dir = MapKeyToDirection(Key)).HasValue)
                        return Dir;
                return null;
            }
        }

        /// <summary>
        /// Handles all updates for the map this tick.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            Keys[] NewKeyState = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in NewKeyState)
            {
                if (!PressedKeys.Contains(key))
                {
                    //new key is pressed
                    Log.Info("Key pressed: " + key);
                    KeyDown(key, time);
                }
                else
                {
                    HandleContinuedKeyPress(key, time);
                }
            }

            foreach(Keys key in PressedKeys)
            {
                if(!NewKeyState.Contains(key))
                {
                    KeyUp(key, time);
                }
            }
            
            PressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        /// <summary>
        /// Handles map logic for the press of a single key
        /// </summary>
        /// <param name="Key"></param>
        private void KeyDown(Keys Key, GameTime GameTime)
        {
            Direction? MoveDirection = MapKeyToDirection(Key);

            if(!IsAiming && MoveDirection.HasValue)
                map.MovePlayer(MoveDirection.Value);

        }

        /// <summary>
        /// Handles map logic for the press of a single key
        /// </summary>
        /// <param name="Key"></param>
        private void KeyUp(Keys Key, GameTime GameTime)
        {
            Direction? ShootDir = MapKeyToDirection(Key);
            if (ShootDir.HasValue && IsAiming)
                map.TryShootTowards(ShootDir.Value);
        }

        /// <summary>
        /// Called continuously as the key is held down.
        /// </summary>
        /// <param name="key"></param>
        private void HandleContinuedKeyPress(Keys key, GameTime GameTime)
        {
            int SpeedIncrement = (PlayerVelocity * GameTime.ElapsedGameTime.TotalSeconds).ToInt();
            switch (key)
            {
                case Keys.Up:
                    map.PlayerLocation.Y -= SpeedIncrement;
                    break;
                case Keys.Down:
                    map.PlayerLocation.Y += SpeedIncrement;
                    break;
                case Keys.Right:
                    map.PlayerLocation.X += SpeedIncrement;
                    break;
                case Keys.Left:
                    map.PlayerLocation.X -= SpeedIncrement;
                    break;
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
