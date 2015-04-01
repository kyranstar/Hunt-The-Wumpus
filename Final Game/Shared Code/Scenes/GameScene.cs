using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using HuntTheWumpus.SharedCode;
using HuntTheWumpus.SharedCode.Helpers;

using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface.Input;
using HuntTheWumpus.SharedCode.GameControl;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class GameScene : Scene
    {
        Map Map;
        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            // Ideally, the Map would have a reset method, but this should work for now.
            Map = new Map();
        }

        public override void Update(GameTime GameTime)
        {

        }

        public override void Draw(GameTime GameTime, SpriteBatch TargetBatch)
        {
            //TODO: Refactor most drawing code to somewhere else

            // Should use Cave.GetCave() instead when it is implemented and exposed
            Room[] Rooms = new Room[]
            {
                new Room()
                {
                    number = 0,
                    // Using a "square" pattern for now to simplify initial test code
                    connections = new int[] {-1, 1, -1, -1}
                },
                new Room()
                {
                    number = 1,
                    connections = new int[] {2, -1, -1, 0}
                },
                new Room()
                {
                    number = 2,
                    connections = new int[] {-1, -1, 1, -1}
                }
            };

            Room CurrentRoom = Rooms[0];
            var R = GetRoomLayout(CurrentRoom, new Vector2(), Rooms.ToDictionary(Room => Room.number));

        }

        /// <summary>
        /// Lays out the given map by converting the room's individual connections into absolute positions
        /// </summary>
        /// <param name="CurrentRoom">The room to start at</param>
        /// <param name="CurrentPoint">The position that the current room should be at</param>
        /// <param name="UnmappedRooms">The list of the unmapped rooms, indexed by ID</param>
        /// <returns>A mapping of room IDs to their room's positions</returns>
        private Dictionary<int, Vector2> GetRoomLayout(Room CurrentRoom, Vector2 CurrentPoint, Dictionary<int, Room> UnmappedRooms)
        {
            Log.Info("GetRoomLayout called for room " + CurrentRoom.number + " at point " + CurrentPoint + " with " + UnmappedRooms.Count + " unmapped rooms");

            // Start with an empty result
            Dictionary<int, Vector2> NewMappedRooms = new Dictionary<int, Vector2>();

            // Iterate over the connections (the index in the array indicates the side)
            for(int ConnectionDirection = 0; ConnectionDirection < CurrentRoom.connections.Length; ConnectionDirection++)
            {
                // Get the ID of the current room
                int ConnectedRoomID = CurrentRoom.connections[ConnectionDirection];

                Room NextRoom;
                // Only process the room if it hasn't been processed already
                if(UnmappedRooms.TryGetValue(ConnectedRoomID, out NextRoom))
                {
                    // Get the point for the next room
                    Vector2 NextPoint = CurrentPoint + GetOffsetForSide(ConnectionDirection, 20f);
                    // Remove the room now that we have calculated its position
                    //   we don't want the next call to index it again
                    UnmappedRooms.Remove(ConnectedRoomID);

                    // Recurse through the connections of the next room
                    Dictionary<int, Vector2> MappedRooms = GetRoomLayout(NextRoom, NextPoint, UnmappedRooms);

                    // Add the current room to the deeper map
                    MappedRooms.Add(NextRoom.number, NextPoint);
                    // Merge the result with the results from the other connections
                    NewMappedRooms = NewMappedRooms.MergeLeft(MappedRooms);
                    
                }
            }

            return NewMappedRooms;
        }

        private Vector2 GetOffsetForSide(int Side, float Apothem)
        {
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            double Angle = -(Math.PI / 2) * Side + (Math.PI / 2);
            return new Vector2(
                (float)Math.Cos(Angle) * Apothem * 2f,
                (float)Math.Sin(Angle) * Apothem * 2f);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
