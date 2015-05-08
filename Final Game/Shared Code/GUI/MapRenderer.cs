using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
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

        private Texture2D PlayerTexture;
        private Texture2D WumpusTexture;

        private Sprite2D Player;
        private Sprite2D Wumpus;

        private const int VirtualViewHeight = 500;
        private const int PlayerHeight = 500;
        private const int WumpusHeight = 500;

        public const int NumCloudTextures = 1,
            NumDoorTextures = 5,
            NumRoomTextures = 5,
            NumPitTextures = 1,
            NumGoldTextures = 0;

        ParticleSystem.ParticleSystem backFogSystem;
        ParticleSystem.FogOfWar frontFogSystem;
        private Texture2D[] CloudTextures,
            ClosedDoorTextures,
            RoomBaseTextures,
            PitTextures,
            GoldTextures;

        public MapRenderer(Map Map)
        {
            this.Map = Map;
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
                    Zoom = 0.16f
                };

            Player = new Sprite2D(PlayerTexture)
            {
                RenderWidth = (int)Math.Round(PlayerHeight / (double)PlayerTexture.Height * PlayerTexture.Width),
                RenderHeight = PlayerHeight
            };

            Wumpus = new Sprite2D(WumpusTexture)
            {
                RenderWidth = (int)Math.Round(WumpusHeight / (double)WumpusTexture.Height * WumpusTexture.Width),
                RenderHeight = PlayerHeight
            };

            UpdateCamera();

            backFogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam);
            frontFogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam);
            frontFogSystem.Opacity = 0.12f;

            backFogSystem.Initialize();
            frontFogSystem.Initialize();
        }

        /// <summary>
        /// Loads the media content for the map (should be called ONCE).
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            PlayerTexture = Content.Load<Texture2D>("Images/Character");
            WumpusTexture = Content.Load<Texture2D>("Images/Wumpus");

            MapUtils.LoadTexturesIntoArray(out CloudTextures, NumCloudTextures, "Cloud", Content);
            MapUtils.LoadTexturesIntoArray(out ClosedDoorTextures, NumDoorTextures, "ClosedDoor", Content);
            MapUtils.LoadTexturesIntoArray(out RoomBaseTextures, NumRoomTextures, "RoomBase", Content);
            MapUtils.LoadTexturesIntoArray(out PitTextures, NumPitTextures, "Pit", Content);
            MapUtils.LoadTexturesIntoArray(out GoldTextures, NumGoldTextures, "Gold", Content);
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime time)
        {
            // TODO: Clean up this math
            Player.RenderX = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Player.HalfWidth) + Map.PlayerLocation.X - 250;
            Player.RenderY = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Player.HalfHeight) + Map.PlayerLocation.Y - 230;

            UpdateCamera();
            UpdateWumpus();

            backFogSystem.Update(time);
            frontFogSystem.Update(time);
        }

        private void UpdateWumpus()
        {
            // TODO: Figure out where we store character position
            Wumpus.RenderX = (int)Math.Round(Map.Cave.RoomLayout[Map.Wumpus.Location].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Wumpus.HalfWidth);
            Wumpus.RenderY = (int)Math.Round(Map.Cave.RoomLayout[Map.Wumpus.Location].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Wumpus.HalfHeight);

            // TODO: Hide Wumpus when it shouldn't be shown
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

            backFogSystem.Draw(MapRenderTarget);

            DrawCaveBase(MapRenderTarget);

            frontFogSystem.Draw(MapRenderTarget);


            Player.Draw(MapRenderTarget);
            Wumpus.Draw(MapRenderTarget);

            MapRenderTarget.End();
        }

        private void DrawCaveBase(SpriteBatch Target)
        {
            // TODO: Figure out if we can move some of this math into "Sprite"s

            if (RoomBaseTextures.Length <= 0 || Map.Cave.RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

            //TODO: Combine these two loops
            // Iterate over each layout mapping
            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in
                (from Mapping in Map.Cave.RoomLayout where Map.PlayerPath.Contains(Mapping.Key) select Mapping))
            {
                // Get the position from the mapping (and round it)
                int XPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.Y);

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);
                Target.Draw(RoomBaseTextures[LayoutMapping.Value.Image], RoomTargetArea, Color.White);

                if (LayoutMapping.Value.PitImage >= 0)
                    Target.Draw(PitTextures[LayoutMapping.Value.PitImage], RoomTargetArea, Color.White);

                if (LayoutMapping.Value.GoldImage >= 0)
                    Target.Draw(GoldTextures[LayoutMapping.Value.GoldImage], RoomTargetArea, Color.White);

                // Iterate over the (closed) door mappings for the current room
                foreach (DoorLayoutMapping DoorMapping in LayoutMapping.Value.ClosedDoorMappings)
                {
                    // Calculate the destination rectangle for the door mapping
                    Rectangle TargetSectionArea = new Rectangle()
                    {
                        X = (int)Math.Round(DoorMapping.Position.X),
                        Y = (int)Math.Round(DoorMapping.Position.Y),
                        Width = Map.Cave.TargetRoomWidth / 2, // TODO: Figure out integer inaccuracies
                        Height = Map.Cave.TargetRoomHeight / 2

                    };

                    // Draw the door texture
                    Target.Draw(
                        ClosedDoorTextures[DoorMapping.BaseImage],
                        destinationRectangle: TargetSectionArea,
                        rotation: DoorMapping.Rotation,
                        color: Color.White);
                }
            }
            // Draw black adjacent rooms
            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in
               (from Mapping in Map.Cave.RoomLayout
                where !Map.PlayerPath.Contains(Mapping.Key)
                    && Map.PlayerPath.Select(i => Map.Cave.GetRoom(i))
                        .Any(r => r.AdjacentRooms.Contains(Mapping.Key))
                select Mapping))
            {
                // Get the position from the mapping (and round it)
                int XPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.Y);

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);

                Color DrawColor = new Color(50, 50, 50, 5);

                // Highlight this room if the user is aiming into it
                if (Map.InputHandler.IsAiming && Map.Cave[Map.PlayerRoom].AdjacentRooms.Contains(LayoutMapping.Key))
                    DrawColor = new Color(150, 150, 150, 10);

                // Highlight this room w/ brighter color if the user has shot into it
                Map.Direction? ShootDirection = Map.InputHandler.NavDirection;
                if (Map.InputHandler.IsAiming && ShootDirection.HasValue)
                {
                    int RoomAtShootDirection = Map.Cave[Map.PlayerRoom].AdjacentRooms[(int)ShootDirection.Value];
                    if (RoomAtShootDirection  == LayoutMapping.Key)
                        DrawColor = new Color(255, 150, 150, 30);
                }

                Target.Draw(RoomBaseTextures[LayoutMapping.Value.Image], RoomTargetArea, DrawColor);
            }
        }

        public int FogParticleCount
        {
            get { return backFogSystem.NumberParticles + frontFogSystem.NumberParticles; }
        }

    }
}
