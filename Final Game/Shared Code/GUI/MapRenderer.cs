﻿using HuntTheWumpus.SharedCode.GameControl;
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
        // Render pipeline
        private GraphicsDevice Graphics;
        private SpriteBatch MapRenderTarget;

        public Camera2D MapCam;
        private Vector2 VirtualViewSize;

        private Map Map;

        // Textures
        private Texture2D PlayerTexture;
        private Texture2D WumpusTexture;
        private Texture2D BackgroundTexture;
        private Texture2D DebugOutlineTexture;
        private Texture2D[] CloudTextures,
            ClosedDoorTextures,
            RoomBaseTextures,
            PitTextures,
            GoldTextures;

        // Sprites and drawing helpers
        private Sprite2D Player;
        private Sprite2D Wumpus;
        private TiledTexture BackgroundTiles;

        public Rectangle? DebugOutline = null;

        // Consts
        private const int VirtualViewHeight = 500;
        private readonly int PlayerHeight;
        private const int WumpusHeight = 500;

        public const int NumCloudTextures = 1,
            NumDoorTextures = 5,
            NumRoomTextures = 4,
            NumPitTextures = 1,
            NumGoldTextures = 0;

        ParticleSystem.ParticleSystem backFogSystem;
        ParticleSystem.FogOfWar frontFogSystem;

        public MapRenderer(Map Map)
        {
            this.Map = Map;
            PlayerHeight = (Map.Cave.TargetRoomHeight * 0.7).ToInt();
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

            BackgroundTiles = new TiledTexture(BackgroundTexture, MapCam);

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
            frontFogSystem.Opacity = 0.3f;

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
            BackgroundTexture = Content.Load<Texture2D>("Images/Background");
            DebugOutlineTexture = Content.Load<Texture2D>("Images/Outline");

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
            BackgroundTiles.Update(time);

            // TODO: Clean up this math
            Player.RenderX = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].PrimaryRoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Player.HalfWidth) + Map.PlayerLocation.X;
            Player.RenderY = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].PrimaryRoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Player.HalfHeight) + Map.PlayerLocation.Y;

            UpdateCamera();
            UpdateWumpus();

            backFogSystem.Update(time);
            frontFogSystem.Update(time);
        }

        private void UpdateWumpus()
        {
            // TODO: Figure out where we store character position
            Wumpus.RenderX = (int)Math.Round(Map.Cave.RoomLayout[Map.Wumpus.Location].PrimaryRoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Wumpus.HalfWidth);
            Wumpus.RenderY = (int)Math.Round(Map.Cave.RoomLayout[Map.Wumpus.Location].PrimaryRoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Wumpus.HalfHeight);

            // TODO: Hide Wumpus when it shouldn't be shown
        }

        private void UpdateCamera()
        {
            Vector2 CameraPosition = new Vector2()
            {
                // TODO: Clean up this math
                X = -(Player.RenderX + Player.HalfWidth - MapCam.VirtualVisibleViewport.Width / 2),
                Y = -(Player.RenderY + Player.HalfHeight - MapCam.VirtualVisibleViewport.Height / 2)
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
            MapRenderTarget.Begin(transformMatrix: MapCam.GetTransform(), samplerState: SamplerState.LinearWrap);

            BackgroundTiles.Draw(MapRenderTarget);
            backFogSystem.Draw(MapRenderTarget);

            DrawCaveBase(MapRenderTarget);

            frontFogSystem.Draw(MapRenderTarget);


            Player.Draw(MapRenderTarget);
            Wumpus.Draw(MapRenderTarget);

            if (DebugOutline.HasValue)
                MapRenderTarget.Draw(DebugOutlineTexture, DebugOutline.Value, Color.White);

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
                int XPos = (int)Math.Round(LayoutMapping.Value.PrimaryRoomPosition.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.PrimaryRoomPosition.Y);

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
                DrawAdjacentRoom(LayoutMapping.Value.PrimaryRoomPosition, LayoutMapping.Key, LayoutMapping.Value.Image, Target);
                foreach (Vector2 PhantomPosition in LayoutMapping.Value.PhantomPositions)
                {
                    DrawAdjacentRoom(PhantomPosition, LayoutMapping.Key, LayoutMapping.Value.Image, Target);
                }
            }
        }

        private void DrawAdjacentRoom(Vector2 Position, int RoomID, int BaseImageNumber, SpriteBatch Target)
        {
            // Get the position from the mapping (and round it)
            int XPos = (int)Math.Round(Position.X);
            int YPos = (int)Math.Round(Position.Y);

            // Calculate the target room rectangle and draw the texture
            Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);

            Color DrawColor = new Color(50, 50, 50, 5);

            // Highlight this room if the user is aiming into it
            if (Map.InputHandler.IsAiming && Map.Cave[Map.PlayerRoom].AdjacentRooms.Contains(RoomID))
                DrawColor = new Color(150, 150, 150, 10);

            // Highlight this room w/ brighter color if the user has shot into it
            Map.Direction? ShootDirection = Map.InputHandler.NavDirection;
            if (Map.InputHandler.IsAiming && ShootDirection.HasValue)
            {
                int RoomAtShootDirection = Map.Cave[Map.PlayerRoom].AdjacentRooms[(int)ShootDirection.Value];
                if (RoomAtShootDirection == RoomID)
                {
                    // Use different color if they will hit the wumpus
                    if (RoomAtShootDirection == Map.Wumpus.Location)
                    {
                        DrawColor = new Color(0, 255, 0, 60);
                    }
                    else
                        DrawColor = new Color(255, 0, 0, 60);
                }
            }

            Target.Draw(RoomBaseTextures[BaseImageNumber], RoomTargetArea, DrawColor);
        }

        public int FogParticleCount
        {
            get { return backFogSystem.NumberParticles + frontFogSystem.NumberParticles; }
        }

    }
}
