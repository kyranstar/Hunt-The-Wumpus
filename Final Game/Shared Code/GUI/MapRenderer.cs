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

        private Sprite2D Player;

        private const int VirtualViewHeight = 500;
        private const int PlayerSize = 500;

        public const int NumCloudTextures = 1,
            NumDoorTextures = 5,
            NumRoomTextures = 5,
            NumPitTextures = 0,
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

            // TODO: Remove this when we are done debugging (set the overridden position to null)
            //OverriddenCameraPosition = new Vector2(1200, 1000);
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
                RenderWidth = PlayerSize,
                RenderHeight = PlayerSize
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


            backFogSystem.Update(time);
            frontFogSystem.Update(time);
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

            DrawPlayer(MapRenderTarget);

            MapRenderTarget.End();
        }

        private void DrawCaveBase(SpriteBatch Target)
        {
            // TODO: Figure out if we can move some of this math into "Sprite"s

            if (RoomBaseTextures.Length <= 0 || Map.Cave.RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

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
        }

        private void DrawPlayer(SpriteBatch Target)
        {
            Player.Draw(Target);
        }

        public int FogParticleCount
        {
            get { return backFogSystem.NumberParticles + frontFogSystem.NumberParticles; }
        }

    }
}
