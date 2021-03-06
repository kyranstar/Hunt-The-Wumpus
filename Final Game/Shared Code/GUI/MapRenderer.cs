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
        private GameController GameController;

        // Textures
        private Texture2D PlayerTexture;
        private Texture2D WumpusTexture;
        private Texture2D BackgroundTexture;
        private Texture2D DebugOutlineTexture;
        private Texture2D[] CloudTextures,
            ClosedDoorTextures,
            RoomBaseTextures,
            PitTextures,
            GoldTextures,
            BatTextures;

        //Fonts
        private SpriteFont Font;

        // Sprites and drawing helpers
        private Sprite2D Player;
        private Sprite2D Wumpus;
        private TiledTexture BackgroundTiles;

        public Rectangle? DebugOutline = null;

        Dictionary<int, StateAnimator> GoldFadeAnimators,
            BatFadeAnimators;

        // Consts
        private const int VirtualViewHeight = 500;
        private readonly int PlayerHeight;
        private readonly int WumpusHeight;

        public const int NumCloudTextures = 2,
            NumDoorTextures = 1,
            NumRoomTextures = 1,
            NumPitTextures = 1,
            NumGoldTextures = 1,
            NumBatTextures = 1;


        public ParticleSystem.ParticleSystem BackFogSystem;
        public ParticleSystem.FogOfWar FrontFogSystem;

        public bool ParticleSystemsEnabled = true;

        byte AimColorHighlightAmount = 120;
        byte AimAlphaHighlightAmount = 120;

        const float DefaultCamZoom = 0.2f;
        const float TrappedInPitCamZoom = 0.65f;

        private StateAnimator CamZoomAnimator;

        public MapRenderer(GameController GameController)
        {
            this.GameController = GameController;
            Map = GameController.Map;

            PlayerHeight = (Map.Cave.TargetRoomHeight * 0.4).ToInt();
            WumpusHeight = (Map.Cave.TargetRoomHeight * 0.4).ToInt();
        }

        /// <summary>
        /// Gets or sets the manually-set camera position (for debugging)
        /// </summary>
        public Vector2? OverriddenCameraPosition { get; set; }

        /// <summary>
        /// Gets or sets the manually-set camera zoom (for debugging)
        /// </summary>
        public float? OverriddenCameraZoom { get; set; }

        /// <summary>
        /// Initializes the renderer for a new game.
        /// </summary>
        /// <param name="Graphics"></param>
        public void Initialize(GraphicsDevice Graphics)
        {
            MapRenderTarget = new SpriteBatch(Graphics);
            this.Graphics = Graphics;

            GoldFadeAnimators = new Dictionary<int, StateAnimator>();
            BatFadeAnimators = new Dictionary<int, StateAnimator>();
            CamZoomAnimator = new StateAnimator(
                pct => (float)pct.Scale(0, 1, DefaultCamZoom, TrappedInPitCamZoom),
                pct => (float)pct.Scale(0, 1, TrappedInPitCamZoom, DefaultCamZoom),
                1.2);
            OverriddenCameraZoom = null;

            Viewport RenderViewport = Graphics.Viewport;
            VirtualViewSize = new Vector2(RenderViewport.AspectRatio * VirtualViewHeight, VirtualViewHeight);
            this.MapCam = new Camera2D(this.VirtualViewSize, RenderViewport)
            {
                Zoom = DefaultCamZoom
            };

            BackgroundTiles = new TiledTexture(BackgroundTexture, MapCam);

            Player = new Sprite2D(PlayerTexture)
            {
                RenderWidth = (PlayerHeight / (double)PlayerTexture.Height * PlayerTexture.Width).ToInt(),
                RenderHeight = PlayerHeight
            };

            // Must set this after initial player creation so that
            // the player's width/height can be accessed
            Player.Position = GetPlayerTargetPosition();

            Player.Initialize();

            Player.AddAnimation(AnimationType.MoveToNewRoom, new SpriteMoveAnimation(dist => dist / 300f));
            Player.StartAnimation(AnimationType.MoveToNewRoom);

            Wumpus = new Sprite2D(WumpusTexture)
            {
                RenderWidth = (WumpusHeight / (double)WumpusTexture.Height * WumpusTexture.Width).ToInt(),
                RenderHeight = WumpusHeight
            };

            Wumpus.Initialize();

            UpdateCamera();

            BackFogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam);
            FrontFogSystem = new ParticleSystem.FogOfWar(CloudTextures, MapCam);
            FrontFogSystem.Opacity = 0f;

            BackFogSystem.Initialize();
            FrontFogSystem.Initialize();
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

            MapUtils.LoadTexturesIntoArray(out CloudTextures, NumCloudTextures, "Cloud", Content, "Images/");
            MapUtils.LoadTexturesIntoArray(out ClosedDoorTextures, NumDoorTextures, "ClosedDoor", Content, "Images/");
            MapUtils.LoadTexturesIntoArray(out RoomBaseTextures, NumRoomTextures, "RoomBase", Content, "Images/");
            MapUtils.LoadTexturesIntoArray(out PitTextures, NumPitTextures, "Pit", Content, "Images/");
            MapUtils.LoadTexturesIntoArray(out GoldTextures, NumGoldTextures, "RoomGoldOverlay", Content, "Images/");
            MapUtils.LoadTexturesIntoArray(out BatTextures, NumBatTextures, "Bat", Content, "Images/");

            Font = Content.Load<SpriteFont>("Arcadepix_40_Bold");
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime time)
        {
            BackgroundTiles.Update(time);

            SpriteMoveAnimation Animation = Player.GetAnimation(AnimationType.MoveToNewRoom) as SpriteMoveAnimation;
            // Currently, setting the target position on this animation saves its current position as the new starting
            // point. This means that as it gets closer to the target, the overall distance that the animation needs to
            // travel decreases. This creates the "easing" that we see, but it isn't being done in an obvious way.
            // TODO: Fix this and remove the above comment essay
            Animation.TargetPosition = GetPlayerTargetPosition();

            Player.Update(time);

            UpdateCamera();
            UpdateWumpus();

            if (ParticleSystemsEnabled && BackFogSystem != null && FrontFogSystem != null)
            {
                BackFogSystem.Update(time);
                FrontFogSystem.Update(time);
            }

            UpdateRoomAnimators(GoldFadeAnimators, r => r.Gold > 0, time);
            UpdateRoomAnimators(BatFadeAnimators, r => r.HasBats, time);

            CamZoomAnimator.Update(time, Map.Cave[Map.PlayerRoom].HasPit);
            MapCam.Zoom = OverriddenCameraZoom ?? CamZoomAnimator.CurrentValue;
        }

        public void UpdateRoomAnimators(Dictionary<int, StateAnimator> Animators, Func<Room, bool> ValueSelector, GameTime time)
        {
            foreach (Room Room in Map.Cave.Rooms)
            {
                if (Animators.ContainsKey(Room.RoomID))
                    continue;

                Animators.Add(Room.RoomID, new StateAnimator(Pct => (float)Pct, Pct => 1 - (float)Pct, 1));
            }

            foreach (var AnimatorMap in Animators)
            {
                AnimatorMap.Value.Update(time, ValueSelector(Map.Cave[AnimatorMap.Key]));
            }
        }

        private void UpdateWumpus()
        {
            // TODO: Figure out where we store character position
            Wumpus.RenderX = (Map.Cave.RoomLayout[Map.Wumpus.Location].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Wumpus.HalfWidth).ToInt();
            Wumpus.RenderY = (Map.Cave.RoomLayout[Map.Wumpus.Location].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Wumpus.HalfHeight).ToInt();

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

        private Vector2 GetPlayerTargetPosition()
        {
            // TODO: Clean up this math
            float PlayerTargetX = Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.X + (Map.Cave.TargetRoomWidth / 2f) - Player.HalfWidth + Map.PlayerRoomLocation.X;
            float PlayerTargetY = Map.Cave.RoomLayout[Map.PlayerRoom].RoomPosition.Y + (Map.Cave.TargetRoomHeight / 2f) - Player.HalfHeight + Map.PlayerRoomLocation.Y;
            return new Vector2(PlayerTargetX, PlayerTargetY);
        }

        /// <summary>
        /// Draws the map.
        /// </summary>
        /// <param name="GameTime"></param>
        public void Draw(GameTime GameTime)
        {
            MapRenderTarget.Begin(transformMatrix: MapCam.GetTransform(), samplerState: SamplerState.LinearWrap);

            BackgroundTiles.Draw(MapRenderTarget);
            if (ParticleSystemsEnabled && BackFogSystem != null)
                BackFogSystem.Draw(MapRenderTarget);

            DrawCaveBase(MapRenderTarget);
            DrawHazards(MapRenderTarget);

            if (ParticleSystemsEnabled && FrontFogSystem != null)
                FrontFogSystem.Draw(MapRenderTarget);

            Player.Draw(MapRenderTarget);
            if(Map.PlayerPath.Contains(Map.Wumpus.Location) || Map.WumpusAlwaysVisisble)
                Wumpus.Draw(MapRenderTarget);

            if (DebugOutline.HasValue)
                MapRenderTarget.Draw(DebugOutlineTexture, DebugOutline.Value, Color.White);

            MapRenderTarget.End();
        }
        private void DrawHazards(SpriteBatch Target)
        {

            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in Map.Cave.RoomLayout.Where(r => Map.PlayerPath.Contains(r.Key)))
            {
                // Get the position from the mapping (and round it)
                int XPos = LayoutMapping.Value.RoomPosition.X.ToInt();
                int YPos = LayoutMapping.Value.RoomPosition.Y.ToInt();

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);

                if (LayoutMapping.Value.PitImage >= 0)
                    Target.Draw(PitTextures[LayoutMapping.Value.PitImage], RoomTargetArea, Color.White);

                if (LayoutMapping.Value.GoldImage >= 0 && GoldFadeAnimators.ContainsKey(LayoutMapping.Key))
                    Target.Draw(GoldTextures[LayoutMapping.Value.GoldImage], RoomTargetArea, ColorUtils.FromAlpha(GoldFadeAnimators[LayoutMapping.Key].CurrentValue));

                if (LayoutMapping.Value.BatImage >= 0 && BatFadeAnimators.ContainsKey(LayoutMapping.Key))
                    Target.Draw(BatTextures[LayoutMapping.Value.BatImage], RoomTargetArea, ColorUtils.FromAlpha(BatFadeAnimators[LayoutMapping.Key].CurrentValue));
            }
        }

        private void DrawCaveBase(SpriteBatch Target)
        {
            // TODO: Figure out if we can move some of this math into "Sprite"s

            if (RoomBaseTextures.Length <= 0 || Map.Cave.RoomLayout == null)
                Log.Error("Textures and cave layout must be loaded before the cave can be drawn.");

            // Iterate over each layout mapping
            foreach (KeyValuePair<int, RoomLayoutMapping> LayoutMapping in Map.Cave.RoomLayout)
            {
                // Get the position from the mapping (and round it)
                int XPos = LayoutMapping.Value.RoomPosition.X.ToInt();
                int YPos = LayoutMapping.Value.RoomPosition.Y.ToInt();

                // Calculate the target room rectangle and draw the texture
                Rectangle RoomTargetArea = new Rectangle(XPos, YPos, Map.Cave.TargetRoomWidth, Map.Cave.TargetRoomHeight);

                bool InDirectPath = Map.PlayerPath.Contains(LayoutMapping.Key);
                bool InSecondaryPath = Map.PlayerPath.Values.Select(i => Map.Cave[i]).Any(r => r.AdjacentRooms.Contains(LayoutMapping.Key));

                Color BaseDrawColor = new Color(128, 128, 128);
                if (!InDirectPath && InSecondaryPath)
                    BaseDrawColor = new Color(50, 50, 50, 5);

                // Highlight this room if the user is aiming into it
                if (GameController.InputHandler.IsAiming && Map.Cave[Map.PlayerRoom].AdjacentRooms.Contains(LayoutMapping.Key))
                {
                    BaseDrawColor.R += AimColorHighlightAmount;
                    BaseDrawColor.G += AimColorHighlightAmount;
                    BaseDrawColor.B += AimColorHighlightAmount;
                    //BaseDrawColor.A += AimAlphaHighlightAmount;
                }

                // Highlight this room w/ brighter color if the user has shot into it
                Direction? ShootDirection = GameController.InputHandler.NavDirection;
                if (GameController.InputHandler.IsAiming && ShootDirection.HasValue)
                {
                    int RoomAtShootDirection = Map.Cave[Map.PlayerRoom].AdjacentRooms[(int)ShootDirection.Value];
                    if (RoomAtShootDirection == LayoutMapping.Key)
                    {
                        if (RoomAtShootDirection == Map.Wumpus.Location)
                        {
                            BaseDrawColor.R += 150;
                        }
                        else
                            BaseDrawColor.G += 150;
                    }
                }

                if (InDirectPath)
                {
                    Target.Draw(RoomBaseTextures[LayoutMapping.Value.Image], RoomTargetArea, BaseDrawColor);

                    // Iterate over the (closed) door mappings for the current room
                    foreach (DoorLayoutMapping DoorMapping in LayoutMapping.Value.ClosedDoorMappings)
                    {
                        // Calculate the destination rectangle for the door mapping
                        Rectangle TargetSectionArea = new Rectangle()
                        {
                            X = DoorMapping.Position.X.ToInt(),
                            Y = DoorMapping.Position.Y.ToInt(),
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
                else if (InSecondaryPath)
                {
                    Target.Draw(RoomBaseTextures[LayoutMapping.Value.Image], RoomTargetArea, BaseDrawColor);
                }

                Vector2 TextSize = Font.MeasureString(LayoutMapping.Key.ToString());
                Target.DrawString(Font, LayoutMapping.Key.ToString(), RoomTargetArea.Center.ToVector2(), ColorUtils.FromAlpha(0.2f), 0, TextSize / 2, 2, SpriteEffects.None, 0);
            }
        }

        public int FogParticleCount
        {
            get { return (BackFogSystem == null ? 0 : BackFogSystem.NumberParticles) + (FrontFogSystem == null ? 0 : FrontFogSystem.NumberParticles); }
        }
    }
}
