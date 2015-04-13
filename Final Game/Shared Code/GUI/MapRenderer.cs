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
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GUI;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class MapRenderer
    {
        private GraphicsDevice Graphics;
        private SpriteBatch MapRenderTarget;

        private Camera2D MapCam;

        private Map Map;

        private Texture2D RoomBaseTexture;
        private Texture2D PlayerTexture;

        private Sprite2D Player;

        // Length of the apothem of each room as it should be drawn
        private readonly double RoomBaseApothem;
        private readonly int RoomNumSides;
        private readonly int TargetRoomWidth, TargetRoomHeight;

        private const int PlayerSize = 500;

        public MapRenderer(Map Map, int RoomNumSides = 6, double RoomBaseApothem = 300)
        {
            this.MapCam = new Camera2D
                {
                    Zoom = 0.16f
                };

            this.Map = Map;

            this.RoomBaseApothem = RoomBaseApothem;
            this.RoomNumSides = RoomNumSides;

            this.TargetRoomWidth = (int)Math.Round(MathUtils.PolygonWidth(RoomNumSides, RoomBaseApothem));
            this.TargetRoomHeight = (int)Math.Round(MathUtils.PolygonHeight(RoomNumSides, RoomBaseApothem));
        }

        /// <summary>
        /// Gets the calculated positions for the available room IDs
        /// </summary>
        public Dictionary<int, Vector2> RoomLayout
        {
            get;
            protected set;
        }

        /// <summary>
        /// Updates the internal layout calculations (adapts to new cave connections)
        /// </summary>
        public void RegenerateLayout()
        {
            RoomLayout = GetRoomLayout(Map.Cave.getRoomList().ToArray());
        }

        public void Initialize(GraphicsDevice Graphics)
        {
            MapRenderTarget = new SpriteBatch(Graphics);
            this.Graphics = Graphics;

            Player = new Sprite2D(PlayerTexture)
            {
                RenderWidth = PlayerSize,
                RenderHeight = PlayerSize
            };
        }

        public void LoadContent(ContentManager Content)
        {
            RoomBaseTexture = Content.Load<Texture2D>("Images/RoomBase");
            PlayerTexture = Content.Load<Texture2D>("Images/Character");
        }

        public void Update()
        {
            // TODO: Clean up this math
            Player.RenderX = (int)Math.Round(RoomLayout[Map.PlayerRoom].X + (TargetRoomWidth / 2f) - Player.HalfWidth);
            Player.RenderY = (int)Math.Round(RoomLayout[Map.PlayerRoom].Y + (TargetRoomHeight / 2f) - Player.HalfHeight);

            UpdateCamera();
        }

        private void UpdateCamera()
        {
            Vector2 CameraPosition = new Vector2()
            {
                // TODO: Clean up this math
                X = 1200,
                Y = 1000
                //X = -(Player.RenderX + Player.HalfWidth - Graphics.Viewport.Width / 2 / MapCam.Zoom),
                //Y = -(Player.RenderY + Player.HalfHeight - Graphics.Viewport.Height / 2 / MapCam.Zoom)
            };

            MapCam.Position = CameraPosition;
        }

        public void Draw(GameTime GameTime)
        {
            MapRenderTarget.Begin(transformMatrix: MapCam.GetTransform());

            DrawCaveBase(MapRenderTarget);
            DrawPlayer(MapRenderTarget);

            MapRenderTarget.End();
        }

        private void DrawCaveBase(SpriteBatch Target)
        {
            // TODO: Figure out if we can move some of this math into "Sprite"s

            if (RoomBaseTexture == null || RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

            foreach (KeyValuePair<int, Vector2> LayoutMapping in RoomLayout)
            {
                int XPos = (int)Math.Round(LayoutMapping.Value.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.Y);

                Rectangle TargetArea = new Rectangle(XPos, YPos, TargetRoomWidth, TargetRoomHeight);
                Target.Draw(RoomBaseTexture, TargetArea, Color.White);
            }
        }

        private void DrawPlayer(SpriteBatch Target)
        {
            Player.Draw(Target);
        }

        /// <summary>
        /// Lays out the given map by converting the room's individual connections into absolute positions
        /// </summary>
        /// <param name="Rooms">The rooms in the map</param>
        /// <returns>A mapping of room IDs to their room's positions</returns>
        private Dictionary<int, Vector2> GetRoomLayout(Room[] Rooms)
        {
            Dictionary<int, Room> UnmappedRooms = Rooms.ToDictionary(Room => Room.roomId);
            Dictionary<int, Vector2> NewLayout = GetRoomLayout(Rooms[0], new Vector2(), UnmappedRooms);

            // If not all rooms were found, we know that not all of them have a valid connection
            if(UnmappedRooms.Count > 0)
                Log.Warn("Some rooms have no valid connections and have not been positioned! The following rooms haven't been connected: " + String.Join(", ", UnmappedRooms.Keys.ToArray()));

            return NewLayout;
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
            Log.Info("GetRoomLayout called for room " + CurrentRoom.roomId + " at point " + CurrentPoint + " with " + UnmappedRooms.Count + " unmapped rooms");

            // Start with an empty result
            Dictionary<int, Vector2> NewMappedRooms = new Dictionary<int, Vector2>();

            // Iterate over the connections (the index in the array indicates the side)
            for (int ConnectionDirection = 0; ConnectionDirection < CurrentRoom.adjacentRooms.Length; ConnectionDirection++)
            {
                // Get the ID of the current room
                int ConnectedRoomID = CurrentRoom.adjacentRooms[ConnectionDirection];

                Room NextRoom;
                // Only process the room if it hasn't been processed already
                if (UnmappedRooms.TryGetValue(ConnectedRoomID, out NextRoom))
                {
                    // If we have gotten to this next room by reference but the next room
                    //   does not have a connection back to the first one, warn of issues!
                    if (!NextRoom.adjacentRooms.Contains(CurrentRoom.roomId))
                        Log.Warn("Room " + CurrentRoom.roomId + " claims it is connected to room " + NextRoom.roomId + ", but the inverse connection was not found!");

                    // Get the point for the next room
                    Vector2 NextPoint = CurrentPoint + GetOffsetForSide(ConnectionDirection, RoomBaseApothem);
                    // Remove the room now that we have calculated its position
                    //   we don't want the next call to index it again
                    UnmappedRooms.Remove(ConnectedRoomID);

                    // Recurse through the connections of the next room
                    Dictionary<int, Vector2> MappedRooms = GetRoomLayout(NextRoom, NextPoint, UnmappedRooms);

                    // Add the current room to the deeper map
                    MappedRooms.Add(NextRoom.roomId, NextPoint);
                    // Merge the result with the results from the other connections
                    NewMappedRooms = NewMappedRooms.MergeLeft(MappedRooms);

                }
            }

            return NewMappedRooms;
        }

        private Vector2 GetOffsetForSide(int Side, double Apothem)
        {
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            double Angle = (Math.PI / 2) - (Math.PI * 2d / RoomNumSides) * Side;
            return new Vector2(
                (float)Math.Cos(Angle) * (float)Apothem * 2f,
                (float)Math.Sin(Angle) * (float)Apothem * -2f);
        }
    }
}
