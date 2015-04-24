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

        private const int VirtualViewHeight = 500;
        private const int PlayerSize = 500;

        ParticleSystem.ParticleSystem fogSystem;
        private List<Texture2D> CloudTextures;

        public MapRenderer(Map Map)
        {
            this.Map = Map;

            // TODO: Remove this when we are done debugging (set the overridden position to null)
            OverriddenCameraPosition = new Vector2(1200, 1000);
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

            fogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam, (p) =>
            {
                int centerX = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f));
                int centerY = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f));

                return !new List<Vector2> 
                { 
                    p.Center.ToVector2(), 
                    new Vector2(p.X, p.Y), 
                    new Vector2(p.X + p.Width, p.Y),
                    new Vector2(p.X + p.Width, p.Y + p.Height),
                    new Vector2(p.X, p.Y + p.Height),
                }.Any((v) => MathUtils.IsInsideHexagon(v, new Vector2(centerX, centerY), Map.Cave.TargetRoomWidth / 2, Map.Cave.TargetRoomHeight / 2));
            });

            fogSystem.Initialize();
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
            Player.RenderX = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Player.HalfWidth) + Map.PlayerLocation.X;
            Player.RenderY = (int)Math.Round(Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Player.HalfHeight) + Map.PlayerLocation.Y;

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

            if (RoomBaseTexture == null || Map.Cave.RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

            // Iterate over each layout mapping
            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in Map.Cave.RoomLayout)
            {
                // Get the position from the mapping (and round it)
                int XPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.X);
                int YPos = (int)Math.Round(LayoutMapping.Value.RoomPosition.Y);

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);
                Target.Draw(RoomBaseTexture, RoomTargetArea, Color.White);

                // Iterate over the (closed) door mappings for the current room
                foreach (Tuple<Vector2, float> DoorMapping in LayoutMapping.Value.ClosedDoorMappings)
                {
                    // Calculate the destination rectangle for the door mapping
                    Rectangle TargetSectionArea = new Rectangle()
                    {
                        X = (int)Math.Round(DoorMapping.Item1.X),
                        Y = (int)Math.Round(DoorMapping.Item1.Y),
                        Width = Map.Cave.TargetRoomWidth / 2, // TODO: Figure out integer inaccuracies
                        Height = Map.Cave.TargetRoomHeight / 2

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

    }
}
