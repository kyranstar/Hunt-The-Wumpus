using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class MapRenderer
    {
        private GraphicsDevice Graphics;
        private SpriteBatch MapRenderTarget;

        private Camera2D MapCam;
        private Vector2 VirtualViewSize;

        private Map Map;

        private Texture2D RoomBaseTexture;
        private Texture2D RoomClosedDoorTexture;
        private Texture2D PlayerTexture;

        private Sprite2D Player;

        private SpriteFont Font;

        private FrameCounter FramerateCounter = new FrameCounter();

        // Length of the apothem of each room as it should be drawn
        private readonly double RoomBaseApothem;
        private readonly int RoomNumSides;
        private readonly int TargetRoomWidth, TargetRoomHeight;

        private const int VirtualViewHeight = 500;
        private const int PlayerSize = 500;

        ParticleSystem.ParticleSystem fogSystem;
        private List<Texture2D> CloudTextures;

        public MapRenderer(Map Map, int RoomNumSides = 6, double RoomBaseApothem = 300)
        {
            this.Map = Map;

            this.RoomBaseApothem = RoomBaseApothem;
            this.RoomNumSides = RoomNumSides;

            this.TargetRoomWidth = (int)Math.Round(MathUtils.PolygonWidth(RoomNumSides, RoomBaseApothem));
            this.TargetRoomHeight = (int)Math.Round(MathUtils.PolygonHeight(RoomNumSides, RoomBaseApothem));

            // TODO: Remove this when we are done debugging (set the overridden position to null)
            OverriddenCameraPosition = new Vector2(1200, 1000);
        }

        /// <summary>
        /// Gets the calculated positions for the available room IDs
        /// </summary>
        public Dictionary<int, RoomLayoutMapping> RoomLayout
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the manually-set camera position (for debugging)
        /// </summary>
        public Vector2? OverriddenCameraPosition { get; set; }

        /// <summary>
        /// Gets or sets the manually-set camera zoom (for debugging)
        /// </summary>
        public float CameraZoom
        {
            get
            {
                return MapCam.Zoom;
            }

            set
            {
                MapCam.Zoom = value;
            }
        }

        /// <summary>
        /// Updates the internal layout calculations (adapts to new cave connections)
        /// </summary>
        public void RegenerateLayout()
        {
            RoomLayout = GetRoomLayout(Map.Cave.getRoomList().ToArray());
        }

        /// <summary>
        /// Initializes the renderer for a new game.
        /// </summary>
        /// <param name="Graphics"></param>
        public void Initialize(GraphicsDevice Graphics)
        {
            MapRenderTarget = new SpriteBatch(Graphics);
            this.Graphics = Graphics;

            Viewport RenderViewport = Graphics.Viewport;
            VirtualViewSize = new Vector2(RenderViewport.AspectRatio * VirtualViewHeight, VirtualViewHeight);
            this.MapCam = new Camera2D(this.VirtualViewSize, RenderViewport)
                {
                    //Zoom = 0.16f
                };

            Player = new Sprite2D(PlayerTexture)
            {
                RenderWidth = PlayerSize,
                RenderHeight = PlayerSize
            };
            fogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam, (p) =>
            {
                int radius = 500;
                return (p.X - Player.RenderX) * (p.X - Player.RenderX) + (p.Y - Player.RenderY) * (p.Y - Player.RenderY) >= radius * radius;
            });
        }

        /// <summary>
        /// Loads the media content for the map (should be called ONCE).
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            RoomBaseTexture = Content.Load<Texture2D>("Images/RoomBase");
            RoomClosedDoorTexture = Content.Load<Texture2D>("Images/ClosedDoor");
            PlayerTexture = Content.Load<Texture2D>("Images/Character");

            Font = Content.Load<SpriteFont>("Segoe_UI_9_Regular");

            CloudTextures = new List<Texture2D>();
            CloudTextures.Add(Content.Load<Texture2D>("Images/cloud"));
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime time)
        {
            // TODO: Clean up this math
            Player.RenderX = (int)Math.Round(RoomLayout[Map.PlayerRoom].RoomPosition.X + (TargetRoomWidth / 2f) - Player.HalfWidth) + Map.PlayerLocation.X;
            Player.RenderY = (int)Math.Round(RoomLayout[Map.PlayerRoom].RoomPosition.Y + (TargetRoomHeight / 2f) - Player.HalfHeight) + Map.PlayerLocation.Y;

            fogSystem.Update(time);

            UpdateCamera();
        }

        private void UpdateCamera()
        {
            Vector2 CameraPosition = new Vector2()
            {
                // TODO: Clean up this math
                X = -(Player.RenderX + Player.HalfWidth - Graphics.Viewport.Width / 2 / MapCam.Zoom),
                Y = -(Player.RenderY + Player.HalfHeight - Graphics.Viewport.Height / 2 / MapCam.Zoom)
            };

            if (OverriddenCameraPosition.HasValue)
            {
                CameraPosition.X = OverriddenCameraPosition.Value.X;
                CameraPosition.Y = OverriddenCameraPosition.Value.Y;
            }

            MapCam.Position = CameraPosition;
        }

        /// <summary>
        /// Draws the map.
        /// </summary>
        /// <param name="GameTime"></param>
        public void Draw(GameTime GameTime)
        {
            MapRenderTarget.Begin(transformMatrix: MapCam.GetTransform());

            DrawCaveBase(MapRenderTarget);
            DrawPlayer(MapRenderTarget);
            fogSystem.Draw(MapRenderTarget);

            MapRenderTarget.End();

            MapRenderTarget.Begin();

            FramerateCounter.Update((float)GameTime.ElapsedGameTime.TotalSeconds);
            var fps = string.Format("FPS: {0}", FramerateCounter.AverageFramesPerSecond);
            var particles = string.Format("Particles: {0}", fogSystem.NumberParticles);
            MapRenderTarget.DrawString(Font, fps, new Vector2(5, 1), Color.Black);
            MapRenderTarget.DrawString(Font, particles, new Vector2(5, 10), Color.Black);

            MapRenderTarget.End();
        }

        private void DrawCaveBase(SpriteBatch Target)
        {
            // TODO: Figure out if we can move some of this math into "Sprite"s

            if (RoomBaseTexture == null || RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

            // Iterate over each layout mapping
            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in RoomLayout)
            {
                // Get the position from the mapping (and round it)
                int XPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.Y);

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, TargetRoomWidth, TargetRoomHeight);
                Target.Draw(RoomBaseTexture, RoomTargetArea, Color.White);

                // Iterate over the (closed) door mappings for the current room
                foreach (Tuple<Vector2, float> DoorMapping in LayoutMapping.Value.ClosedDoorMappings)
                {
                    // Calculate the destination rectangle for the door mapping
                    Rectangle TargetSectionArea = new Rectangle()
                    {
                        X = (int)Math.Round(DoorMapping.Item1.X),
                        Y = (int)Math.Round(DoorMapping.Item1.Y),
                        Width = TargetRoomWidth / 2, // TODO: Figure out integer inaccuracies
                        Height = TargetRoomHeight / 2

                    };

                    // Draw the door texture
                    Target.Draw(
                        RoomClosedDoorTexture,
                        destinationRectangle: TargetSectionArea,
                        rotation: DoorMapping.Item2,
                        color: Color.White);
                }
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
        private Dictionary<int, RoomLayoutMapping> GetRoomLayout(Room[] Rooms)
        {
            Dictionary<int, Room> UnmappedRooms = Rooms.ToDictionary(Room => Room.roomId);
            Dictionary<int, RoomLayoutMapping> NewLayout = GetRoomLayout(Rooms[0], new Vector2(), UnmappedRooms);

            // If not all rooms were found, we know that not all of them have a valid connection
            if (UnmappedRooms.Count > 0)
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
        private Dictionary<int, RoomLayoutMapping> GetRoomLayout(Room CurrentRoom, Vector2 CurrentPoint, Dictionary<int, Room> UnmappedRooms)
        {
            Log.Info("GetRoomLayout called for room " + CurrentRoom.roomId + " at point " + CurrentPoint + " with " + UnmappedRooms.Count + " unmapped rooms");

            // Start with an empty result
            Dictionary<int, RoomLayoutMapping> NewMappedRooms = new Dictionary<int, RoomLayoutMapping>();

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

                    RoomLayoutMapping NextMapping = new RoomLayoutMapping()
                    {
                        Room = NextRoom
                    };

                    // Get the point for the next room
                    NextMapping.RoomPosition = CurrentPoint + GetOffsetForSide(ConnectionDirection, RoomBaseApothem * 2, RoomNumSides);

                    // Get the list of poses for the non-connection overlays (closed doors)
                    NextMapping.ClosedDoorMappings = MapDoorsForRoom(NextRoom.adjacentRooms, NextMapping.RoomPosition);

                    // Remove the room now that we have calculated its position
                    //   we don't want the next call to index it again
                    UnmappedRooms.Remove(ConnectedRoomID);

                    // Recurse through the connections of the next room
                    Dictionary<int, RoomLayoutMapping> MappedRooms = GetRoomLayout(NextRoom, NextMapping.RoomPosition, UnmappedRooms);

                    // Add the current room to the deeper map
                    MappedRooms.Add(NextRoom.roomId, NextMapping);
                    // Merge the result with the results from the other connections
                    NewMappedRooms = NewMappedRooms.MergeLeft(MappedRooms);

                }
            }

            return NewMappedRooms;
        }

        /// <summary>
        /// Gets the position and rotation (pose) of each closed door for the room info.
        /// </summary>
        /// <param name="Connections">The set of connections that the given room has</param>
        /// <param name="RoomOrigin">The position of the given room</param>
        /// <returns>A mapping of positions and rotations for each closed door</returns>
        private Tuple<Vector2, float>[] MapDoorsForRoom(int[] Connections, Vector2 RoomOrigin)
        {
            List<Tuple<Vector2, float>> DoorMappings = new List<Tuple<Vector2, float>>();

            foreach (int Direction in
                Connections
                .Select((RoomID, Index) => new KeyValuePair<int, int>(Index, RoomID)) // Map the connections to <Direction, RoomID>
                .Where(AdjacentRoomMapping => AdjacentRoomMapping.Value == -1) // Only select the non-connections
                .Select(Pair => Pair.Key)) // Convert it back to an array of directions
            {

                // Get the offsets for the current direction
                Vector2 Offset = GetOffsetForSectionRadius(Direction, RoomBaseApothem, RoomNumSides);
                Vector2 CenterRoom = new Vector2()
                {
                    X = RoomOrigin.X + TargetRoomWidth / 2,
                    Y = RoomOrigin.Y + TargetRoomHeight / 2
                };

                Vector2 DoorIconPosition = new Vector2()
                {
                    X = CenterRoom.X + Offset.X,
                    Y = CenterRoom.Y + Offset.Y
                };

                // Get the rotation to make the wedge fit corectly
                float DoorIconRotation = -GetAngleForSide(Direction, RoomNumSides) + ((float)Math.PI * 0.5f);

                DoorMappings.Add(new Tuple<Vector2, float>(DoorIconPosition, DoorIconRotation));
            }

            // Can't use yield return because we need an array 
            return DoorMappings.ToArray();
        }

        /// <summary>
        /// Calculates a vector that describes the given side's middle point, relative to the center.
        /// </summary>
        /// <param name="Side">The side index</param>
        /// <param name="Apothem">The measure of the apothem of the polygon.</param>
        /// <param name="NumSides">The number of sides on the polygon.</param>
        /// <returns>A vector representing the given values.</returns>
        public static Vector2 GetOffsetForSide(int Side, double Apothem, int NumSides)
        {
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            float Angle = GetAngleForSide(Side, NumSides);
            return MathUtils.PolarToCart(Angle, Apothem);
        }

        /// <summary>
        /// Calculates the angle that points to the center of the specified side.
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        public static float GetAngleForSide(int Side, int NumSides)
        {
            double d = (Math.PI / 2f) - (Math.PI * 2f / NumSides) * Side;
            return (float)MathUtils.Mod(d, (Math.PI * 2));
        }

        /// <summary>
        /// Calculates a vector that describes the given side's corner point, relative to the center.
        /// </summary>
        /// <param name="Side">The side index.</param>
        /// <param name="Apothem">The measure of the apothem of the polygon.</param>
        /// <returns></returns>
        public static Vector2 GetOffsetForSectionRadius(int Side, double Apothem, int NumSides)
        {
            // TODO: Find a better name
            // TODO: Look into managing floating-point inaccuracies
            // Assuming 'North' is side 0
            // Assuming side 0 is the NW radius line
            float Angle = GetAngleForSectionRadius(Side, NumSides);
            double Radius = MathUtils.PolygonRadius(NumSides, Apothem);
            return MathUtils.PolarToCart(Angle, Radius);
        }

        /// <summary>
        /// Calculates the angle that points to the corner of the specified side.
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        public static float GetAngleForSectionRadius(int Side, int NumTotalSides)
        {
            double SingleSectionAngle = Math.PI * 2f / NumTotalSides;
            double Angle = (Math.PI / 2f + SingleSectionAngle / 2f) - SingleSectionAngle * Side;
            return (float)(MathUtils.Mod(Angle, Math.PI * 2));
        }
    }

    /// <summary>
    /// Holds render information about a single room.
    /// </summary>
    public class RoomLayoutMapping
    {
        public Room Room;
        public Vector2 RoomPosition;
        public Tuple<Vector2, float>[] ClosedDoorMappings;
    }
}
