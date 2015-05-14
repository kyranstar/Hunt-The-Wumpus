using HuntTheWumpus.SharedCode.GameControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GameMap
{
    /// <summary>
    /// A class that handles inputs for the map.
    /// </summary>
    public class MapInputHandler
    {
        public const int PlayerVelocity = 600;

        private Map map;
        private Keys[] PressedKeys = {};

        public bool IsAiming
        {
            get
            {
                return PressedKeys.Contains(Keys.LeftShift);
            }
        }

        public Map.Direction? NavDirection
        {
            get
            {
                // TODO: Clean up this logic
                Map.Direction? Dir;
                foreach (Keys Key in PressedKeys)
                    if ((Dir = MapKeyToDirection(Key)).HasValue)
                        return Dir;
                return null;
            }
        }

        public MapInputHandler(Map map)
        {
            this.map = map;
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
            Map.Direction? MoveDirection = MapKeyToDirection(Key);

            if(!IsAiming && MoveDirection.HasValue)
                map.MovePlayer(MoveDirection.Value);

        }

        /// <summary>
        /// Handles map logic for the press of a single key
        /// </summary>
        /// <param name="Key"></param>
        private void KeyUp(Keys Key, GameTime GameTime)
        {
            Map.Direction? ShootDir = MapKeyToDirection(Key);
            if (ShootDir.HasValue && this.IsAiming)
                map.TryShootTowards(ShootDir.Value);
        }

        /// <summary>
        /// Called continuously as the key is held down.
        /// </summary>
        /// <param name="key"></param>
        private void HandleContinuedKeyPress(Keys key, GameTime GameTime)
        {
            int SpeedIncrement = (int)Math.Round(PlayerVelocity * GameTime.ElapsedGameTime.TotalSeconds);
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

        private Map.Direction? MapKeyToDirection(Keys Key)
        {
            switch (Key)
            {
                case Keys.W:
                    return Map.Direction.North;
                case Keys.E:
                    return Map.Direction.Northeast;
                case Keys.D:
                    return Map.Direction.Southeast;
                case Keys.S:
                    return Map.Direction.South;
                case Keys.A:
                    return Map.Direction.Southwest;
                case Keys.Q:
                    return Map.Direction.Northwest;
                default:
                    return null;
            }
        }
    }
}
