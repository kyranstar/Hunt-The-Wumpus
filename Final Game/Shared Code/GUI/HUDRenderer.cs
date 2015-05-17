using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using EmptyKeys.UserInterface.Generated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class HudRenderer
    {
        // Render pipeline
        private GraphicsDevice Graphics;
        private SpriteBatch HudRenderTarget;
        private HUDOverlayView HudView;

        // Fonts and textures
        private SpriteFont UIFont9;

        // Measurement and state
        private FrameCounter FramerateCounter = new FrameCounter();
        private MapRenderer MapRanderer;
        private GameController GameController;

        public HudRenderer(MapRenderer MapRenderer, GameController GameController)
        {
            this.MapRanderer = MapRenderer;
            this.GameController = GameController;
        }

        /// <summary>
        /// Initializes the renderer for a new game.
        /// </summary>
        /// <param name="Graphics"></param>
        public void Initialize(GraphicsDevice Graphics)
        {
            HudRenderTarget = new SpriteBatch(Graphics);
            this.Graphics = Graphics;

            HudView = new HUDOverlayView(Graphics.Viewport.Width, Graphics.Viewport.Height);
            HudView.DataContext = new HUDContext(GameController);
        }

        /// <summary>
        /// Loads the media content (should be called ONCE).
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            UIFont9 = Content.Load<SpriteFont>("Segoe_UI_9_Regular");
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime Time)
        {
            HudView.UpdateInput(Time.ElapsedGameTime.TotalMilliseconds);
            HudView.UpdateLayout(Time.ElapsedGameTime.TotalMilliseconds);
        }

        /// <summary>
        /// Draws the HUD.
        /// </summary>
        /// <param name="GameTime"></param>
        public void Draw(GameTime GameTime)
        {
            HudView.Draw(GameTime.ElapsedGameTime.TotalMilliseconds);

            HudRenderTarget.Begin();

            FramerateCounter.Update((float)GameTime.ElapsedGameTime.TotalSeconds);
            DrawDebugInfo();

            HudRenderTarget.End();
        }

        private void DrawDebugInfo()
        {
            // TODO: Make this math use a target rectangle for debug info
            //  instead of hard-coded magic numbers

            // Render general info
            string FPS = string.Format("FPS: {0}", FramerateCounter.AverageFramesPerSecond);
            string Particles = string.Format("Fog particles: {0}", MapRanderer.FogParticleCount);
            HudRenderTarget.DrawString(UIFont9, FPS, new Vector2(5, 1), Color.White);
            HudRenderTarget.DrawString(UIFont9, Particles, new Vector2(5, 11), Color.White);

            // Render room status
            string Room = GameController.Map.Cave[GameController.Map.PlayerRoom].ToString();
            HudRenderTarget.DrawString(UIFont9, Room, new Vector2(5, 21), Color.White);
        }
    }
}
