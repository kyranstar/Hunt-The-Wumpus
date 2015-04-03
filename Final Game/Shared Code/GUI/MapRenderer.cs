using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GUI
{
    class MapRenderer
    {
        private Map Map;
        private Dictionary<int, Vector2> RoomLayout;

        private Texture2D RoomBaseTexture;

        // Length of the apothem of each room
        private const int RoomBaseApothem = 20;
        // Currently using square for simplicity
        private const int RoomNumSides = 4;
        //TODO: Fix and commit polygon width calculation
        private readonly int RoomBaseSize = 40;///(int)Math.Round(MathUtils.PolygonWidth(RoomNumSides, RoomBaseApothem));

        public MapRenderer(Map Map)
        {
            this.Map = Map;
        }

        /// <summary>
        /// Updates the internal layout calculations (adapts to new cave connections)
        /// </summary>
        public void RegenerateLayout()
        {
            // TODO: Use Cave.GetCave() instead once it has been implemented and exposed
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

            RoomLayout = GetRoomLayout(Rooms);
        }

        public void LoadContent(ContentManager Content)
        {
            RoomBaseTexture = Content.Load<Texture2D>("Images/RoomBase");
        }

        public void DrawCaveBase(SpriteBatch Target)
        {
            if(RoomBaseTexture == null || RoomLayout == null)
                throw new Exception("Textures and cave layout must be loaded before the cave can be drawn.");

            foreach(KeyValuePair<int, Vector2> LayoutMapping in RoomLayout)
            {
                int XPos = (int)Math.Round(LayoutMapping.Value.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.Y);

                Rectangle TargetArea = new Rectangle(XPos, YPos, RoomBaseSize, RoomBaseSize);
                Target.Draw(RoomBaseTexture, TargetArea, Color.White);
            }
        }

        /// <summary>
        /// Lays out the given map by converting the room's individual connections into absolute positions
        /// </summary>
        /// <param name="Rooms">The rooms in the map</param>
        /// <returns>A mapping of room IDs to their room's positions</returns>
        private Dictionary<int, Vector2> GetRoomLayout(Room[] Rooms)
        {
            return GetRoomLayout(Rooms[0], new Vector2(), Rooms.ToDictionary(Room => Room.number));
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
            for (int ConnectionDirection = 0; ConnectionDirection < CurrentRoom.connections.Length; ConnectionDirection++)
            {
                // Get the ID of the current room
                int ConnectedRoomID = CurrentRoom.connections[ConnectionDirection];

                Room NextRoom;
                // Only process the room if it hasn't been processed already
                if (UnmappedRooms.TryGetValue(ConnectedRoomID, out NextRoom))
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
    }
}
